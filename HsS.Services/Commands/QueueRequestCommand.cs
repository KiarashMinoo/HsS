using HsS.Ifs.Cqrs;

namespace HsS.Services.Commands
{
    public class QueueRequestCommand : ICommand
    {
        public object Request { get; set; }
        public string HubId { get; set; }
    }
}
