using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class CreateGroupRequest
    {
        [BodyParam]
        public string Name { get; set; }
    }
}
