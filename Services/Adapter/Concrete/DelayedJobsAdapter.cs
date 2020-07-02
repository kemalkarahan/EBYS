using Entities.ViewModels;
using Hangfire;
using Services.Adapter.Abstract;
using Services.NativeServices.Abstract;
using System;

namespace Services.Adapter.Concrete
{
    public class DelayedJobsAdapter : ScheduledJobService
    {
        public DelayedJobsAdapter(IMessageService messageService) : base(messageService)
        {

        }
        public override string Execute(EmailViewModel message, DateTime finalDate)
        {
            finalDate = finalDate.AddDays(-1);
            double fromDays = (finalDate.Date - DateTime.Now.Date).Days;

            if (fromDays<1)
            {
                return "0";
            }

            string jobId = BackgroundJob.Schedule(
            () => messageService.Default(message),
            TimeSpan.FromDays(fromDays));

            return jobId;
        }
    }
}
