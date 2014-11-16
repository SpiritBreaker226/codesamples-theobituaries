// Forgot User Password

public partial class DesktopModules_MemberForgotPassword_MemberForgotPassword : PortalModuleBase
{
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtUserDetails = DAL.getRow("", "Where  = '" + DAL.safeSql(txtEmail.Text.Trim()) + "' AND  = 1");//holds the users details

            //checks if the if the user is in the database and can login is in the database
            if (dtUserDetails.Rows.Count > 0)
            {
                //sends an email to the user with the link to reset their password
                General.sendHTMLMail(txtEmail.Text, "Your Request to Reset Your Password", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/ForgotPassword.html")), , dtUserDetails.Rows[0][""].ToString(), dtUserDetails.Rows[0][""].ToString()));

				//turn on the thank you message and removes the body
				panThankYou.Visible = true;
				panBody.Visible = false;
            }//end of if
            else
	            throw new Exception("Email could not be found");
        }//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }//end of catch
    }//end of cmdLogin_Click()
}//end of Module()