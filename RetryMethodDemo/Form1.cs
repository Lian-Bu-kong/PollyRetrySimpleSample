using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsRetryDemo.Core;
using WindowsFormsRetryDemo.Device;

namespace WindowsFormsRetryDemo
{
    public partial class Form1 : Form
    {
        private InstrumentDevice _device;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _device = new InstrumentDevice();

        }
        // Normal Method
        private static void CustomSimpleRetryMethod(Action action, int retryCount = 3, int waitMinSecond = 1)
        {
            while (true)
            {
                try
                {
                    action();
                    break;
                }
                catch (Exception e)
                {
                    if (--retryCount == 0)
                    {
                        MessageBox.Show("Retry 3 times fail=>" + e.Message);
                        break;
                    }

                    SpinWait.SpinUntil(() => false, waitMinSecond);
                }
            }
        }
        private void button_normal_retry_Click(object sender, EventArgs e)
        {
            CustomSimpleRetryMethod(() =>
            {
                richTextBox_Info.AppendText("Normal Retry\n");
                _device.ReadData(50);
            });
        }

        private void button_PollyRetry_Click(object sender, EventArgs e)
        {
            int readDelayMillisecond = 50;
            bool testReadEnd = false;

            // Define Retry strategy (No delay between retry)
            var retryPolicy = Policy.Handle<Exception>()
                .Retry(retryCount: 3, (expection, retryCount) =>
                 {
                     // Log exception
            
                     // 延遲讀取Delay時間
                     readDelayMillisecond += 10;

                     if (retryCount == 3)
                         testReadEnd = true;
                 });


            // Execute
            TryFlow(() =>
            {
                retryPolicy.Execute(() =>
                {
                    richTextBox_Info.AppendText($"Read Delay {readDelayMillisecond} == Current Read Time {DateTime.Now.ToString("HH:mm:ss:fff")}s\n");
                    _device.ReadData(readDelayMillisecond, testReadEnd);
                    richTextBox_Info.AppendText("Completed Read\n");
                });

            });
        }
        private void button_pollyRetryWithDelay_Click(object sender, EventArgs e)
        {

            int readDelayMillisecond = 50;
            bool testReadEnd = false;

            // Define Retry strategy (With Delay between retry) 
            var retryPolicy = Policy.Handle<Exception>()
               .WaitAndRetry(retryCount: 3, _ => TimeSpan.FromSeconds(1),
               onRetry: (expection, timeSpan, retryCount, context) =>
               {
                   // Process before retry
                   // 每次Retry延遲讀取時間
                   readDelayMillisecond += 10;

                   if (retryCount == 3)
                       testReadEnd = true;
               });


            // Execute
            TryFlow(() =>
            {
                retryPolicy.Execute(() =>
                {
                    richTextBox_Info.AppendText($"Read Delay {readDelayMillisecond} == Current Read Time {DateTime.Now.ToString("HH:mm:ss:fff")}s\n");
                    _device.ReadData(readDelayMillisecond, testReadEnd);
                    richTextBox_Info.AppendText("Completed Read\n");
                });

            });
        }

        private async void button_asycn_retry_Click(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();

            // 最簡單的 Retry (Make it work)-最不推薦的方法. 
            //await _device.TestReadAsync(cts.Token);
            //await Task.Delay(TimeSpan.FromSeconds(1));
            //await _device.TestReadAsync(cts.Token);
            //await Task.Delay(TimeSpan.FromSeconds(2));
            //await _device.TestReadAsync(cts.Token);
            //await Task.Delay(TimeSpan.FromSeconds(3));

            // 重構 迴圈+防護 (Make it right)
            //foreach (var i in Enumerable.Range(1, 3))
            //{
            //    var exceptions = new List<Exception>();
            //    try
            //    {
            //        await _device.TestReadAsync(cts.Token);
            //        await Task.Delay(TimeSpan.FromSeconds(i));
            //    }
            //    catch (Exception ex)
            //    {
            //        exceptions.Add(ex);
            //    }
            //    if (exceptions.Any())
            //    {
            //        MessageBox.Show(new AggregateException("Read fail!", exceptions).Message);
            //    }
            //}

            // 重構 抽方法 (Make it fast) 快不一定代表執行的快，也可能是寫得快、容易理解的快
            //try
            //{
            //    await RetryAsync(3, () => _device.TestReadAsync(cts.Token));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            // 既然抽方法了，看看能不能共用，重構成擴充方法
            try
            {
                await _device.TestReadAsync(cts.Token).RetryAsync(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // 非同Polly方法 (示意 Wait Clean Code)
        private async void button_AsyncRetry_Click(object sender, EventArgs e)
        {
      
            var cts = new CancellationTokenSource();
            var retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(new[]
            {
              //第一次Retry前Wait延遲時間
              TimeSpan.FromSeconds(1),
              //第二次Retry前Wait延遲時間
              TimeSpan.FromSeconds(2),
              //第三次Retry前Wait延遲時間
              TimeSpan.FromSeconds(3)
            },
            onRetry: (expection, timeSpan, retryCount, context) =>
            {
                if (retryCount == 3)
                    // 取消讀取
                    cts.Cancel();
            });

            try
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    richTextBox_Info.InvokeIfRequired(() =>
                    {
                        richTextBox_Info.AppendText($"Current Read Time {DateTime.Now.ToString("HH:mm:ss:fff")}s\n");
                    });
                    await _device.TestReadAsync(cts.Token);
                });

                richTextBox_Info.AppendText("Completed Read\n");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
   

     
        private void TryFlow(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static async Task RetryAsync<T>(int retryTimes, Func<Task<T>> funcAsync)
        {
            foreach (var i in Enumerable.Range(1, retryTimes))
            {
                var exceptions = new List<Exception>();
                try
                {
                    await funcAsync();
                    await Task.Delay(TimeSpan.FromSeconds(i));
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
                if (exceptions.Any())
                {
                    // Record Log Or Do Something

                    // For Test Demo just show message
                    MessageBox.Show(new AggregateException("Read fail!", exceptions).Message);
                }
            }
        }

      
    }
}
