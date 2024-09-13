using CurrencyApiDomain.ContextRelated;
using CurrencyApiInfrastructure.DbBase;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyApiDomain.Triggers
{
    /// <summary>
    /// The table base trigger.
    /// </summary>
    public class TableBaseTrigger : IBeforeSaveTrigger<TableBase>
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly AppDbContext _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableBaseTrigger"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TableBaseTrigger(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Before the save.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        public Task BeforeSave(ITriggerContext<TableBase> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType is ChangeType.Added)
            {
                context.Entity.CreatedAt = DateTime.Now;
                context.Entity.CreatedBy = "Assessor";//GetMyUserId();
            }
            else if (context.ChangeType is ChangeType.Modified)
            {
                context.Entity.LastUpdatedAt = DateTime.Now;
                context.Entity.LastUpdatedBy = "Assessor";//GetMyUserId();
            }
            else if (context.ChangeType is ChangeType.Deleted)
            {
                context.Entity.DeletedAt = DateTime.Now;
                context.Entity.DeletedBy = "Assessor";//GetMyUserId();

                _dataContext.Entry(context.Entity).State = EntityState.Modified;
            }

            return Task.CompletedTask;
        }
    }
}
