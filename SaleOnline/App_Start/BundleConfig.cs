using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SaleOnline.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            // add style CSS to bundle for public pages ----------
            bundles.Add(new StyleBundle("~/bundles/css1").Include(
                                                                    "~/Content/boostrap.min.css",
                                                                    "~/Content/font-awesome.min.css",
                                                                    "~/Content/prettyPhoto.css",
                                                                    "~/Content/price-range.css",
                                                                    "~/Content/animate.css",
                                                                    "~/Content/main.css",
                                                                    "~/Content/responsive.css"
                                                                                                 ));
            // add style CSS to bundle for Private pages ----------
            bundles.Add(new StyleBundle("~/bundles/css2").Include(
                                                                  
                                                                   "~/Content/all.min.css",
                                                                   "~/Content/adminlte.min.css",
                                                                   "~/Content/summernote-bs4.css",
                                                                   "~/Content/dataTables.bootstrap4.css"
                                                                                                ));

            // add Script to bundle for Public pages ----------
            bundles.Add(new ScriptBundle("~/bundles/moder").Include(

                                                                   "~/Script/modernizr-2.6.2.js"));
            bundles.Add(new ScriptBundle("~/bundles/Script1").Include(
                                                                   "~/Script/jquery-1.10.2.min.js",
                                                                   "~/Script/bootstrap.min.js",
                                                                   "~/Script/jquery.scrollUp.min.js",
                                                                   "~/Script/price-range.js",
                                                                   "~/Script/jquery.prettyPhoto.js",
                                                                   "~/Script/main.js"
                                                                   ));
            // add Script to bundle for Private pages ----------
            bundles.Add(new ScriptBundle("~/bundles/Script2").Include(
                                                                   "~/Script/jquery.min.js",
                                                                   "~/Script/bootstrap.bundle.min.js",
                                                                   "~/Script/jquery.adminlte.js",
                                                                   "~/Script/demo.js",
                                                                   "~/Script/jquery.dataTables.js",
                                                                   "~/Script/dataTables.bootstrap4.js"
                                                                   ));

        }

    }
}