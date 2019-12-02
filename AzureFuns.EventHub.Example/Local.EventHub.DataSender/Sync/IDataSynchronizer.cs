using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Local.EventHub.DataSender
{
    public interface IDataSynchronizer
    {
        Task Run();
    }
}
