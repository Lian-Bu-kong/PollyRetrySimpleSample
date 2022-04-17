using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsRetryDemo.Core
{
    public static class RetryHelper
    {
      
        public static async Task RetryAsync<T>(this Task<T> funcAsync, int retryTimes)
        {
            foreach (var i in Enumerable.Range(1, retryTimes))
            {
                var exceptions = new List<Exception>();
                try
                {
                    await funcAsync;
                    await Task.Delay(TimeSpan.FromSeconds(i));
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
                if (exceptions.Any())
                {
                    // Record Log
                    
                    // For Test Demo just show message
                    MessageBox.Show(new AggregateException("Read fail!", exceptions).Message);
                }
            }
        }
    }
}
