using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevQuestions.Domain.Report
{
    public enum ReportStatus
    {
        /// <summary>
        /// Статус открыт.
        /// </summary>
        OPEN,

        /// <summary>
        /// Статус в работе.
        /// </summary>
        IN_PROGRESS,

        /// <summary>
        /// Статус решен.
        /// </summary>
        RESOLVED,

        /// <summary>
        /// Статус закрыть.
        /// </summary>
        DISMISSED,
    }
}
