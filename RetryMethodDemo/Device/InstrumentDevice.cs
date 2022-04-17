using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsRetryDemo.Device
{
    public class InstrumentDevice
    {

        public void ReadData(int delayMilTime, bool isTestEnd = false)
        {
            if (isTestEnd)
                return;

            SpinWait.SpinUntil(() => false, delayMilTime);
            throw new Exception("Read Time Out");
        }

        // 正常寫時
        private async Task<byte[]> Rs232ReadAsync(int waitMilliTime)
        {
            var readTimeoutCts = new CancellationTokenSource(TimeSpan.FromMilliseconds(waitMilliTime));
            var waitReadCts = readTimeoutCts.Token;

            var readBuffer = new List<byte>();
  
            while (!waitReadCts.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(50));
                // write read code here (readBuffer)...

            }

            if (readTimeoutCts.IsCancellationRequested)
            {
                throw new Exception("Read Time Out!");
            }
            return readBuffer.ToArray();
        }

        // 測試用
        public async Task<byte[]> TestReadAsync(CancellationToken token)
        {
            // 外部CancellationTokenSource若沒設時間，要下Cancel才會逾期

            // 取消工作由呼叫端決定, 但還是可以加內部的 CancellationToken
            var timeoutToken = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));

            // 將token與timeoutCts token組合成一個聚合token. 當組合中有一個token逾期，此token就會逾期
            var aggregationWaitReadToken = CancellationTokenSource.CreateLinkedTokenSource(timeoutToken.Token, token);

            var readBuffer = new List<byte>();

            while (!aggregationWaitReadToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(50));

                // write read code here (readBuffer)
            }

            // Timeout
            if (timeoutToken.IsCancellationRequested)
            {
                throw new Exception("Read Time Out!");
            }
            return readBuffer.ToArray();
        }

        #region Old Method(捨棄)
        [Obsolete]
        public async Task<byte[]> AsyncRead(int waitMilliTime, bool isTestEnd = false)
        {
            var timeoutCts = new CancellationTokenSource(TimeSpan.FromMilliseconds(waitMilliTime));
            var token = timeoutCts.Token;

            var readBuffer = new List<byte>();

            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(50));

                // write read code here 
                if (isTestEnd)
                    break;

            }
            // Timeout
            if (timeoutCts.IsCancellationRequested)
            {
                throw new Exception("Read Time Out!");
            }
            return readBuffer.ToArray();
        }
        #endregion
    }
}
