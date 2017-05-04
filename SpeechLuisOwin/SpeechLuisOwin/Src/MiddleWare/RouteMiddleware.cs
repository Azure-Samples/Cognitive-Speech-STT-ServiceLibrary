using Microsoft.Owin;
using Microsoft.Owin.Logging;
using System.Threading.Tasks;

namespace SpeechLuisOwin.Src.MiddleWare
{
    public class RouteMiddleware : OwinMiddleware
    {
        private readonly ILogger _logger;


        public RouteMiddleware(OwinMiddleware next, ILogger logger) : base(next)
        {
            this._logger = logger;
        }

        public override async Task Invoke(IOwinContext context)
        {
            //this._logger.Write("Inside the 'Invoke' method of the '{0}' middleware.", GetType().Name);

            await Next.Invoke(context);
        }
    }
}