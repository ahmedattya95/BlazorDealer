#pragma checksum "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\Shared\HeaderMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9500c984bd744dea586da96e1769155ea333eeb2"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorDealer.Admin.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using BlazorDealer.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\dell\source\repos\BlazorDealer\BlazorDealer.Admin\_Imports.razor"
using BlazorDealer.Admin.Shared;

#line default
#line hidden
#nullable disable
    public class HeaderMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<header id=\"header\" class=\"header\">\r\n    <div class=\"top-left\">\r\n        <div class=\"navbar-header\">\r\n            <a class=\"navbar-brand\" href=\"./\"><img src=\"images/logo.png\" alt=\"Logo\"></a>\r\n            <a class=\"navbar-brand hidden\" href=\"./\"><img src=\"images/logo2.png\" alt=\"Logo\"></a>\r\n            <a id=\"menuToggle\" class=\"menutoggle\"><i class=\"fa fa-bars\"></i></a>\r\n        </div>\r\n    </div>\r\n    <div class=\"top-right\">\r\n        <div class=\"header-menu\">\r\n            <div class=\"header-left\">\r\n                <button class=\"search-trigger\"><i class=\"fa fa-search\"></i></button>\r\n                <div class=\"form-inline\">\r\n                    <form class=\"search-form\">\r\n                        <input class=\"form-control mr-sm-2\" type=\"text\" placeholder=\"Search ...\" aria-label=\"Search\">\r\n                        <button class=\"search-close\" type=\"submit\"><i class=\"fa fa-close\"></i></button>\r\n                    </form>\r\n                </div>\r\n\r\n                <div class=\"dropdown for-notification\">\r\n                    <button class=\"btn btn-secondary dropdown-toggle\" type=\"button\" id=\"notification\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\r\n                        <i class=\"fa fa-bell\"></i>\r\n                        <span class=\"count bg-danger\">3</span>\r\n                    </button>\r\n                    <div class=\"dropdown-menu\" aria-labelledby=\"notification\">\r\n                        <p class=\"red\">You have 3 Notification</p>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <i class=\"fa fa-check\"></i>\r\n                            <p>Server #1 overloaded.</p>\r\n                        </a>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <i class=\"fa fa-info\"></i>\r\n                            <p>Server #2 overloaded.</p>\r\n                        </a>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <i class=\"fa fa-warning\"></i>\r\n                            <p>Server #3 overloaded.</p>\r\n                        </a>\r\n                    </div>\r\n                </div>\r\n\r\n                <div class=\"dropdown for-message\">\r\n                    <button class=\"btn btn-secondary dropdown-toggle\" type=\"button\" id=\"message\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\r\n                        <i class=\"fa fa-envelope\"></i>\r\n                        <span class=\"count bg-primary\">4</span>\r\n                    </button>\r\n                    <div class=\"dropdown-menu\" aria-labelledby=\"message\">\r\n                        <p class=\"red\">You have 4 Mails</p>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <span class=\"photo media-left\"><img alt=\"avatar\" src=\"images/avatar/1.jpg\"></span>\r\n                            <div class=\"message media-body\">\r\n                                <span class=\"name float-left\">Jonathan Smith</span>\r\n                                <span class=\"time float-right\">Just now</span>\r\n                                <p>Hello, this is an example msg</p>\r\n                            </div>\r\n                        </a>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <span class=\"photo media-left\"><img alt=\"avatar\" src=\"images/avatar/2.jpg\"></span>\r\n                            <div class=\"message media-body\">\r\n                                <span class=\"name float-left\">Jack Sanders</span>\r\n                                <span class=\"time float-right\">5 minutes ago</span>\r\n                                <p>Lorem ipsum dolor sit amet, consectetur</p>\r\n                            </div>\r\n                        </a>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <span class=\"photo media-left\"><img alt=\"avatar\" src=\"images/avatar/3.jpg\"></span>\r\n                            <div class=\"message media-body\">\r\n                                <span class=\"name float-left\">Cheryl Wheeler</span>\r\n                                <span class=\"time float-right\">10 minutes ago</span>\r\n                                <p>Hello, this is an example msg</p>\r\n                            </div>\r\n                        </a>\r\n                        <a class=\"dropdown-item media\" href=\"#\">\r\n                            <span class=\"photo media-left\"><img alt=\"avatar\" src=\"images/avatar/4.jpg\"></span>\r\n                            <div class=\"message media-body\">\r\n                                <span class=\"name float-left\">Rachel Santos</span>\r\n                                <span class=\"time float-right\">15 minutes ago</span>\r\n                                <p>Lorem ipsum dolor sit amet, consectetur</p>\r\n                            </div>\r\n                        </a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"user-area dropdown float-right\">\r\n                <a href=\"#\" class=\"dropdown-toggle active\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\r\n                    <img class=\"user-avatar rounded-circle\" src=\"images/admin.jpg\" alt=\"User Avatar\">\r\n                </a>\r\n\r\n                <div class=\"user-menu dropdown-menu\">\r\n                    <a class=\"nav-link\" href=\"#\"><i class=\"fa fa- user\"></i>My Profile</a>\r\n\r\n                    <a class=\"nav-link\" href=\"#\"><i class=\"fa fa- user\"></i>Notifications <span class=\"count\">13</span></a>\r\n\r\n                    <a class=\"nav-link\" href=\"#\"><i class=\"fa fa -cog\"></i>Settings</a>\r\n\r\n                    <a class=\"nav-link\" href=\"/logout\"><i class=\"fa fa-power -off\"></i>Logout</a>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</header>");
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
