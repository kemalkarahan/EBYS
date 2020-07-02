using Entities.ViewModels;
using Hangfire;
using Services.NativeServices.Abstract;
using System;

namespace Services.Adapter.Abstract
{
    public abstract class ScheduledJobService
    {
        protected readonly IMessageService messageService;

        public ScheduledJobService(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public abstract string Execute(EmailViewModel message, DateTime finalDate);

        public void DeleteJob(string jobId)
        {
            if (jobId != "0") 
            {
                BackgroundJob.Delete(jobId);
            }
        }
    }
}
