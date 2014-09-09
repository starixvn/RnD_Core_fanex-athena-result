using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;

namespace MvcExample
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullBuilder = new NullBuilder();
            var nullOrderer = new NullOrderer();
            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();

            var css = new Bundle("~/bundles/coreCss")
            .Include("~/Content/jquery-ui/base/all.css")
            .Include("~/Content/bootstrap/bootstrap.less")
           .Include("~/Content/Core/site.less")
              .Include("~/Content/Fanex.Result/fanex.result.css");
            css.Builder = nullBuilder;
            css.Transforms.Add(cssTransformer);
            css.Orderer = nullOrderer;

            bundles.Add(css);

          

            var coreJs = new Bundle("~/bundles/coreJs")
                .Include("~/Scripts/jquery-1.9.0.js",
                "~/Scripts/jquery-ui-1.11.1.js",
                "~/Scripts/bootstrap.js", "~/Scripts/Core/site.js",
                "~/Scripts/Page/normal-report.js",
               "~/Scripts/Page/outright-report.js",
               "~/Scripts/Page/popup-report.js"
                );

            coreJs.Builder = nullBuilder;
            coreJs.Transforms.Add(jsTransformer);
            coreJs.Orderer = nullOrderer;

            bundles.Add(coreJs);

            // If you'd like to test the optimization locally,
            // you can use this line to force it.
            BundleTable.EnableOptimizations = true;
        }
    }
}