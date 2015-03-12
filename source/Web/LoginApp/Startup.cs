using Microsoft.Owin.Extensions;

using Nancy;
using Nancy.Owin;

using Owin;

namespace LoginApp
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseNancy(o => o.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound));
      app.UseStageMarker(PipelineStage.MapHandler);
    }
  }
}
