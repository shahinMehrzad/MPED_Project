# MPED_Project
open up the appsettings.json and change connectionstrings <br/>
Set the Default Project as the Infrastructure project and run the following commands:<br/>
add-migration initial2 -context ApplicationDbContext<br/>
add-migration initialIdentity2 -context IdentityContext<br/><br/>

With the Migrations ready, let’s update the database:<br/>
update-database -context IdentityContext<br/>
update-database -context ApplicationDbContext

<br/><br>
<b>Username : </b>admin@MPED.pl<br>
<b>Password : </b>Admin1234
