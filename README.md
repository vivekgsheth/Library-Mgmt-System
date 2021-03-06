# Library-Mgmt-System
It's a project on Library Management System using Asp.net MVC Framework.










the freelancing project had two users: client and freelancer. A client can create contests and freelancer and view and submit solution for particular contests. When client click on create contest, get request is sent to controller and it displays create contest page. Once the details about contest are filled in, post request is sent to create method of contest controller and creates a contest object, fetches the client id from session and saves the data from request body in the model and redirects to contests of client . ViewBag is a dynamic object to pass the data from Controller to View. And, this will pass the data as a property of object ViewBag. Public ActionResult Index()
{
ViewBag.Title = “Welcome”;
return View();
}

@ViewBag.Title
ViewData is a dictionary object to pass the data from Controller to View where data is passed in the form of key-value pair. Public ActionResult Index()
{
ViewData[”Title”] = “Welcome”;
return View();
}

@ViewData[“Title”]
TempData is a dictionary object to pass the data from one action to other action in the same Controller or different Controllers. Usually, TempData object will be stored in a session object. Tempdata is also required to typecast and for null checking before reading data from it. TempData scope is limited to the next request and if we want Tempdata to be available even further, we should use Keep and peek.

Ex - Controller Public ActionResult Index()
{
TempData[”Data”] = “I am from Index action”;
return View();
}

Public string Get()
{
return TempData[”Data”] ;
}

Difference between mvc and mvt? in MVC, we have to wrute all the controller specific code, but in mvt, the controller part is taken care by the framework. MVT is loosely coupled and easy to modify. in mvc, view is controlled by model and controller. in mvt, view cobtrols the model. Advantage of mvc: SEPERATION OF CONCERNS(SOC)

Routing in routeconfig.cs register in global.asax RouteConfig.RegisterRoutes(RouteTable.Routes);

Scaffolding is an automatic code generation framework for ASP.NET web applications. Scaffolding reduces the time taken to develop a controller, view etc. in MVC framework. You can develop a customized scaffolding template using T4 templates as per your architecture and coding standard.

action method must be public, can not be static cannot be overloaded.

###### Global.asax :
allows you to write code that runs in response to application level events, such as Application_BeginRequest, application_start, application_error, session_start, session_end etc.

## Routing

Route defines the URL pattern and handler information. All the configured routes of an application stored in RouteTable and will be used by Routing engine to determine appropriate handler class or file for an incoming request

 the route is configured using the MapRoute() extension method of RouteCollection, where name is "Default", url pattern is "{controller}/{action}/{id}" and defaults parameter for controller, action method and id parameter. Defaults specifies which controller, action method or value of id parameter should be used if they do not exist in the incoming request URL.
 
  the route is configured using the MapRoute() extension method of RouteCollection, where name is "Default", url pattern is "{controller}/{action}/{id}" and defaults parameter for controller, action method and id parameter. Defaults specifies which controller, action method or value of id parameter should be used if they do not exist in the incoming request URL.
  
  After configuring all the routes in RouteConfig class, you need to register it in the Application_Start() event in the Global.asax. So that it includes all your routes into RouteTable.
  
## Controller

Controller handles incomming URL requests. MVC routing sends request to appropriate controller and action method based on URL and configured Routes.<br/>
All the public methods in the Controller class are called Action methods.<br/>
A Controller class must be derived from System.Web.Mvc.Controller class.<br/>
A Controller class name must end with "Controller".<br/>
New controller can be created using different scaffolding templates. You can create custom scaffolding template also.

## Action methods

Action method must be public. It cannot be private or protected <br/>
Action method cannot be overloaded <br/>
Action method cannot be a static method.<br/>
ActionResult is a base class of all the result type which returns from Action method.<br/>
Base Controller class contains methods that returns appropriate result type e.g. View(), Content(), File(), JavaScript() etc.<br/>
Action method can include Nullable type parameters.<br/>

## Action Selectors
Action selector is the attribute that can be applied to the action methods. It helps routing engine to select the correct action method to handle a particular request <br/>
1.ActionName
2.NonAction
3.ActionVerbs<br/>
ActionName attribute allows us to specify a different action name than the method name.<br/>
ActionVerbs are another Action Selectors which selects an action method based on request methods e.g POST, GET, PUT etc.
Multiple action methods can have same name with different action verbs. Method overloading rules are applicable.
Multiple action verbs can be applied to a single action method using AcceptVerbs attribute.<br/>
For e.g [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)] <br/>

## View
View is a User Interface which displays data and handles user interaction.<br/>
Views folder contains separate folder for each controller.<br/>
ASP.NET MVC supports Razor view engine in addition to traditional .aspx engine.<br/>
Razor view files has .cshtml or .vbhtml extension.<br/>
You can write a mix of html tags and server side code in razor view. Razor uses @ character for server side code instead of traditional <% %>.Razor view engine maximize the speed of writing code by minimizing the number of characters and keystrokes required when writing a view. <br/>

## Model Binding
Model binding automatically converts request values into a primitive or complex type object. Model binding is a two step process. First, it collects values from the incoming http request and second, populates primitive type or complex type with these values.<br/>
Value providers are responsible for collecting values from request and Model Binders are responsible for populating values.<br/>
 The [Bind] attribute will let you specify the exact properties a model binder should include or exclude in binding.
 
## Razor Syntax

Use @ to write server side code.
Server side code block starts with @{* code * }
Use @: or <text></<text> to display text from code block.
if condition starts with @if{ }
for loop starts with @for
@model allows you to use model object anywhere in the view.
 
 ## Html Helpers
 
HtmlHelper class generates html elements using the model class object in razor view. It binds the model object to html elements to display value of model properties into html elements and also assigns the value of the html elements to the model properties while submitting web form.

## Validation

ASP.NET MVC uses DataAnnotations attributes for validation.

DataAnnotations attributes can be applied to the properties of the model class to indicate the kind of value the property will hold

Use ValidationSummary to display all the error messages in the view.

Use ValidationMessageFor or ValidationMessage helper method to display field level error messages in the view.

Check whether the model is valid before updating in the action method using ModelState.IsValid.

Enable client side validation to display error messages without postback effect in the browser.

## ViewBag
ViewBag can be useful when you want to transfer temporary data (which is not included in model) from the controller to the view. 

ViewBag only transfers data from controller to view, not visa-versa. ViewBag values will be null if redirection occurs.

ViewBag doesn't require typecasting while retriving values from it.

ViewBag doesn't require typecasting while retriving values from it.

## ViewData
ViewData is a dictionary which can contain key-value pairs where each key must be string

we must cast ViewData values to the appropriate data type.

ViewData and ViewBag both use the same dictionary internally. So you cannot have ViewData Key matches with the property name of ViewBag, otherwise it will throw a runtime exception

## TempData
TempData in ASP.NET MVC can be used to store temporary data which can be used in the subsequent request. TempData will be cleared out after the completion of a subsequent request.

TempData is useful when you want to transfer non-sensitive data from one action method to another action method of the same or a different controller as well as redirects. It is dictionary type which is derived from TempDataDictionary.

TempData internally uses session to store the data. So the data must be serialized if you decide you to switch away from the default Session-State Mode, and use State Server ModeTempData internally uses session to store the data. So the data must be serialized if you decide you to switch away from the default Session-State Mode, and use State Server Mode

Call TempData.Keep() to retain TempData values in a third consecutive request.








