# GamaLearn_TechnicalAssesement

**I have used the below technology stack to build .NET Core Web API, SignalR Service and Web UI Project(s):-**
        
    a. Backend: .NET 8 Web API
    b. Frontend: React.js with React Bootstrap UI 
    c. Real-time Communication: SignalR
    d. Offline Processing: Custom implementation without Hangfire   
    e. Database: SQL Server
    f. Deployment: Couldnt setup entire deploymemt workflow due to issues with running a paid Parallel-Job for Azure DevOps Pipelines. 
                   But the .yaml configuration is setup under a private project in DevOps portal. 

I have added all the expected chat functionality and a basic chat room feature. Please check the gif added to get a preview of the whole application functionality.

**Please follow the steps to setup the project(s) and to check the key features.**

1) Set "**GamaLearn.ChatApp.Api**" as startup project and then update the below in **appsettings.json** file:-

		a. Update "ChatAppDBConnection" configuration to point to your local SQLServer instance
		b. Update "JwtConfiguration" configuration
			--Set "ValidAudience" to ChatApp Web UI URL
			--Set "ValidIssuer" to ChatApp API UI URL


3) Open Nuget Package Manager console and select "GamaLearn.ChatApp.Infrastructure" as default project. Since migrations are already added into the project, we do not need to add any EF migrations. Please run both of the below command directly:

     	a. update-database -Context AuthDBContext
	    b. update-database -Context ChatAppDBContext

    However, in case of any issues, you can delete the "**GamaLearn.ChatApp.Infrastructure\Migrations**" folder to get EF migrations file again and run the below commands in sequence as listed:
	
       a. Add-Migration Create_Database -Context AuthDBContext -o Migrations/Identity
	   b. update-database -Context AuthDBContext
	   c. Add-Migration UserMessages -Context ChatAppDBContext -o Migrations/Chat
	   d. update-database -Context ChatAppDBContext

    Please open your SSMS IDE and validate if "**ChatAppDB**" is created succesfully or not. It must have all **Identity.AspNet{tables) and also Chat.{tables}** with two seperate DBContext EF_MigrationsHistory   tables.  

3) Go to  "**GamaLearn.ChatApp.SignalRWebpack**" project and then update the below:
   
	    a. In appsettings.json file:-
		       update "ChatAppDBConnection" configuration to point to your local SQLServer instance
     	b. Configure this project also to run as "Multiple Startup" along with .API project
	

4) Please ensure both the **.API & .SignalR** project are up and running on two seperate hosts.

      a. You can use .API\SwaggerUI to test various methods of User API & ChatRooms API directly.

5) Please open "**ChatApp.Frontend\GamaLearn.ChatApp.Web**" in VS Code and then update the below:
   
	    a. Under "\src\utilHelpers\" path, update the below properties in Constants.ts file. Kindly update only the port section in the "https://localhost:{port}" value.
		      - "userApiURL" - pointing to .API project
		      - "chatRoomApiURL" - pointing to .API project
		      - "signalRHubURL" - pointing to .SignalR project

	    b. Go to terminal and perform below commands:
		      - npm install
		      - npm start

    	c. Please ensure after the Web project is hosted, the WEB URL is successfully updated in the "ValidAudience" property inside .API project(as mentioned in step 1.b)
