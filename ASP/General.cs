// holds General Functions for the site

public class General {	
	//gets the caluulation of the tax base on 
	public static string calculateTax(int intCountryID, int intProvinceID, string strSubTotal, bool boolGetTaxTotalOnly = false) {
		string strStateTaxTotal = "0.0";//holds what the tax total for the state will be
		string strCurrentStateTaxRate = "0";//holds the current tax rate of the state
		
		//checks if this the counrtry is canada
        if (intCountryID == 1)
        {			
			//choose which country the user belongs to
            switch (intProvinceID)
            {
                case 1:  //ON
                case 8:  //NB
                case 9:  //NL
                    strCurrentStateTaxRate = "0.13";
                    break;
                case 2:  //QC
				case 3:  //BC
                case 4:  //AB
                case 5:  //MB
                case 6:  //SK
                case 11: //NT
                case 12: //YT
                case 13: //NU
                    strCurrentStateTaxRate = "0.05";
                    break;
                case 10: //PE  
                    strCurrentStateTaxRate = "0.14";
                    break;
                case 7:  //NS
                    strCurrentStateTaxRate = "0.15";
                    break;
            }//end of switch
        }//end of if
		//checks if this the counrtry is US
        else if (intCountryID == 2)
		{
			//choose which country the user belongs to
            switch (intProvinceID)
            {
                default:
                    strCurrentStateTaxRate = "0";
                    break;
            }//end of switch
		}//end of else
		
		//calculates the tax for the state base on strSubTotal
		strStateTaxTotal = (Convert.ToDecimal(strSubTotal) * Convert.ToDecimal(strCurrentStateTaxRate)).ToString();
		
		//checks if boolGetTaxRate is true if so then just send the the tax rate
		if(boolGetTaxTotalOnly == true)
			return strStateTaxTotal;
		else
        	return (Convert.ToDecimal(strSubTotal) + Convert.ToDecimal(strStateTaxTotal)).ToString() + "*" + strStateTaxTotal;
    }//end of calculateTax()
	
	//checks if the user's email already exist in the databasee
	public static bool checkIfEmailExist(string strEmail) {
		DataTable dtUserDetails = DAL.getRow("", "Where  = '" + DAL.safeSql(strEmail.Trim()) + "'");//holds the users details
		
		//checks if there is any rows found if so then the email does exist
		if (dtUserDetails.Rows.Count > 0)
			return true;
		else
			return false;
	}//end of checkIfEmailExist()
		
	//crreates a new user with just a first, last name, email
	public static int createNewUser(string strFName, string strLName, string strEmail, bool boolNewsletter, bool  boolSendEmailToUser, string strEmailTemplateFile = "~/EmailTemplate/thankYouIndividualSignUp.html")
	{
		string strUserPassword = General.genPassword();//holds the random password that will be sent to the user
		string strEmailTemplate = string.Format(File.ReadAllText(HttpContext.Current.Server.MapPath(strEmailTemplateFile)), strFName,  strLName, strEmail, strUserPassword);//holds the email tempalte that will be sent to the user
		int intUserID = DAL.addUpdateUsers(0, strFName, strLName, "", "", "", "", "", "1", "1", "", "", strEmail, PasswordHash.passwordHashtable.createHash(strUserPassword), "", 2, false, boolNewsletter);//holds the new users id just in case they want to create a Funeral Home
		
		//checks if a email needs to be sent to the user
		if(boolSendEmailToUser == true)
			//sends the user an email that the have been add to the database
			//this is so wrong and needs to change however this is what the client whats
			General.sendHTMLMail(strEmail, "Complete Your Registration", strEmailTemplate);
		
		return intUserID;
	}//end of createNewUser()

    //crreates a new user with just a first, last name, email
    public static int createObituaryCoOwner(string strFName, string strLName, string strEmail, bool boolNewsletter, bool boolSendEmailToUser, string strEmailTemplateFile, string obituaryName)
    {
        string strUserPassword = General.genPassword();//holds the random password that will be sent to the user
        string strEmailTemplate = string.Format(File.ReadAllText(HttpContext.Current.Server.MapPath(strEmailTemplateFile)), strFName, strLName, strEmail, strUserPassword, obituaryName);//holds the email tempalte that will be sent to the user
        int intUserID = DAL.addUpdateUsers(0, strFName, strLName, "", "", "", "", "", "1", "1", "", "", strEmail, PasswordHash.passwordHashtable.createHash(strUserPassword), "", 2, false, boolNewsletter);//holds the new users id just in case they want to create a Funeral Home

        //checks if a email needs to be sent to the user
        if (boolSendEmailToUser == true)
            //sends the user an email that the have been add to the database
            //this is so wrong and needs to change however this is what the client whats
            General.sendHTMLMail(strEmail, "Administrative Access to Record", strEmailTemplate);

        return intUserID;
    }// end of createObituaryCoOwner()

    //randomly create a password to be sent an save for the user
	public static string genPassword()
	{
		System.Random ranNumber = new System.Random();//holds the object that will random gen
		string strPassword = "";//holds the string that will have the password
		char chRandom;//holds the random char
		
		//goes around for getting 7 random chars
		for(int intIndex = 0; intIndex < 7; intIndex++)
		{
			//randomly choose a random char
			chRandom = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * ranNumber.NextDouble() + 65)));
			
			//adds to the password
			strPassword += chRandom;
		}//end of for loop
		
		return strPassword;
	}//end of genPassword()
	
	//resizes a image
	public static SD.Image resizeImage(SD.Image imgBigImage, int ingMaxWidth, int ingMaxHeight)
	{
		int intNewWidth = 0;//holds the new width of the new image
		int intNewHeight = 0;//holds the new height of the new image
		int intCurrentWidth = imgBigImage.Width;//holds the current width of the image
		int intCurrentHeight = imgBigImage.Height;//holds the current height of the image

		//checks if the width is larger then the heigth if so then 
		//floors the width and set the height to be apporenit to the width
		if ((intCurrentWidth / (double)ingMaxWidth) > (intCurrentHeight / (double)ingMaxHeight))
		{
			//adjuest the height to be apporent now and floors the width
			intNewWidth = ingMaxWidth;
			intNewHeight = Convert.ToInt32(intCurrentHeight * (ingMaxWidth / (double)intCurrentWidth));
			
			//checks if the new height is not bigger then the max
			if (intNewHeight > ingMaxHeight)
			{
				//adjuest the width to be apporent now and floors the height
				intNewWidth = Convert.ToInt32(ingMaxWidth * (ingMaxHeight / (double)intNewHeight));
				intNewHeight = ingMaxHeight;
			}//end of if
		}//end of if
		//else floors the height and set the width to be apporenit to the height
		else
		{
			//adjuest the width to be apporent now and floors the height
			intNewWidth = Convert.ToInt32(intCurrentWidth * (ingMaxHeight / (double)intCurrentHeight));
			intNewHeight = ingMaxHeight;
			
			//checks if the new width is not bigger then the max
			if (intNewWidth > ingMaxWidth)
			{
				//adjuest the height to be apporent now and floors the width
				intNewWidth = ingMaxWidth;
				intNewHeight = Convert.ToInt32(ingMaxHeight * (ingMaxWidth / (double)intNewWidth));
			}//end of if
		}//end of else

        SD.Bitmap bitNewImage = new SD.Bitmap(intNewWidth, intNewHeight);//holds the new bitmap of the image
		
		//web resolution;
		bitNewImage.SetResolution(72, 72); 

		SD.Graphics grImage = SD.Graphics.FromImage(bitNewImage);//holds the graphics object

        grImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
		//creates a new holder of where the new image will be drawen to
		grImage.FillRectangle(new SD.SolidBrush(SD.Color.White), 0, 0, bitNewImage.Width, bitNewImage.Height);

		//Re-draw the image to the specified height and width
		grImage.DrawImage(imgBigImage, 0, 0, bitNewImage.Width, bitNewImage.Height);

		return bitNewImage;
	}//end of resizeImage()

	//search for City with the same provance if it is found then add to intNumberOf to display currently
	public static int searchCityNumberOf(string strProvName, string strCityName, List<CitySearchItem> listCitySearchItem) {
		try {
			CitySearchItem csiResult = listCitySearchItem.Find(
				delegate(CitySearchItem csi) {
					return csi.SearchProvince == strProvName && csi.SearchCity == strCityName;
				}//end of delegate
			);//holds the results of the search in listCitySearchItem
						
			//checks if thre is any items with the city with the same provance is found 				
			if (csiResult != null)
				//add to intNumberOf
				csiResult.NumberOf++;
			else
				return 0;
						
			return csiResult.NumberOf;
		}//end of try
		catch (Exception ex) {
			throw ex;
		}//end of catch
	}//end of searchCityNumberOf()
	
	//search for provance if it is found then add to intNumberOf to display currently
	public static int searchProvNumberOf(string strProvName, List<CitySearchItem> listCitySearchItem) {
		try {
			CitySearchItem csiResult = listCitySearchItem.Find(
				delegate(CitySearchItem csi)
				{
					return csi.SearchProvince == strProvName;
				}//end of delegate
			);//holds the results of the search in listCitySearchItem
						
			//checks if thre is any items with the city with the same provance is found 				
			if (csiResult != null)
				//add to intNumberOf
				csiResult.NumberOf++;
			else
				return 0;
						
			return csiResult.NumberOf;
		}//end of try
		catch (Exception ex)
		{
			throw ex;
		}//end of catch
	}//end of searchProvNumberOf()
		
	//sends out emails to the user
	public static string sendHTMLMail(string strToAddress, string strSubject, string strBody, string strBCC = "", string strCC = "", string strFromAddress = "", string strFromName = "theObituaries")
	{
		try 
		{
			RestClient client = new RestClient();//holds if the API from mailgun
	   		RestRequest request = new RestRequest();//holds the request to the API
	   
	   		//sets the authenthiozion to using the mailgun API
	   		client.BaseUrl = "https://api.mailgun.net/v2";
       		
			//checks if this is on live or on staging/dev
			if (HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() == "theobituaries.ca" || HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() == "www.theobituaries.ca")
			{
				//sets the API Key
				client.Authenticator = new HttpBasicAuthenticator("api","");
				
				//sets the domain to sent the message to which in turn will send it out to strToAddress
		   		request.AddParameter("domain", "theobituaries.ca", ParameterType.UrlSegment);
			}//end of if
			else {
				// send the email throw mailgun

				//sets the API Key
				client.Authenticator = new HttpBasicAuthenticator("api","");
			
				//sets the domain to sent the message to which in turn will send it out to strToAddress
		   		request.AddParameter("domain", "", ParameterType.UrlSegment);
			}//end of else
			
       		request.Resource = "{domain}/messages";
			
			//sets the from and to address
       		request.AddParameter("from", strFromName + " <" + strFromAddress + ">");
       		request.AddParameter("to", strToAddress);
			
			//checks if there is a cc to use if so then add it to mmSentMessage
			if(!string.IsNullOrEmpty(strCC))
				request.AddParameter("cc", strCC);
				
			//checks if there is a bcc to use if so then add it to mmSentMessage
			if(!string.IsNullOrEmpty(strBCC))
				request.AddParameter("bcc", strBCC);
       					
			//sets the basic items that will be send to the user
			request.AddParameter("subject", strSubject);
       		request.AddParameter("html", strBody);
       		request.Method = Method.POST;
	   
	   		//sends out to the user
	       	client.Execute(request);
		}//end of try
		catch (System.Exception e) 
		{
			// return the text of unknown error
			return e.ToString();
		}//end of catch
		
		return "0";
	}//end of sendHTMLMail()

    //send email with attachment
    public static string sendHTMLMailWithAttachment(string strToAddress, string strSubject, string strBody, string attachmentFilePath, string strFromAddress = "", string strFromName = "theObituaries")
    {
        try
        {
            RestClient client = new RestClient();//holds if the API from mailgun
            RestRequest request = new RestRequest();//holds the request to the API

            //sets the authenthiozion to using the mailgun API
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "");

            //sets the domain to sent the message to which in turn will send it out to strToAddress
            request.AddParameter("domain", "", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";

            //sets the from and to address
            request.AddParameter("from", strFromName + " <" + strFromAddress + ">");
            request.AddParameter("to", strToAddress);            

            //sets the basic items that will be send to the user
            request.AddParameter("subject", strSubject);
            request.AddParameter("html", strBody);
            request.AddFile("attachment", attachmentFilePath);

            request.Method = Method.POST;

            //sends out to the user
            client.Execute(request);            
        }//end of try
        catch (System.Exception e)
        {
            // return the text of unknown error
            return e.ToString();
        }//end of catch

        return "0";
    }//end of sendHTMLMailWithAttachment()

	//sets the session and cookies for user when they login
	public static void setSession(DataTable dtUserDetails, bool usingCookies) {
		//sets the session valuable to tell the that the user has been loged in
		HttpContext.Current.Session["MemberLogin"] = dtUserDetails.Rows[0][""].ToString();
	}//end of setSession()
	
	public static string stripHtml(string strWithHTML) {
		//checks if there is any text to strWithHTML to remove
		if (string.IsNullOrEmpty(strWithHTML))
			return strWithHTML;
		else
			//strips out the HTML
			return System.Text.RegularExpressions.Regex.Replace(strWithHTML, "<[^>]*>", "");
	}//end of stripHtml()
		
	//uploads the Image to the server
	public static string uploadImage(string strSavePath,HttpPostedFile myFile, int intMaxThumbnailWidth = 0, int intMaxThumbnailHeight = 0)
	{
		// Check file size (mustn't be 0)
		int intFileLen = myFile.ContentLength;
		string strImageFileExtension = System.IO.Path.GetExtension(myFile.FileName);//holds the file extension
	
		if (intFileLen == 0)
			return "ERROR! File Length is zero for " + myFile.FileName;

		// Check file extension that it is the internet images .jpg/.gif/.png
		if (strImageFileExtension.ToLower() != ".jpg" && strImageFileExtension.ToLower() != ".png" && strImageFileExtension.ToLower() != ".gif")
			return "ERROR! The Image file must have an extension of either JPG, PNG or GIF: for " + myFile.FileName;
			
		//checks if the file size is above 8MB
		if(intFileLen > 8000000)
			return "ERROR! The Image file most be below 8MB";

		// Read file into a data stream
		byte[] bytData = new Byte[intFileLen];
		myFile.InputStream.Read(bytData,0,intFileLen);

		// Make sure a duplicate file doesn't exist.  If it does, keep on appending an 
		// incremental numeric until it is unique
		string strImageFileName = System.IO.Path.GetFileName(myFile.FileName);
		int file_append = 0;
		
		//checks if the the file name is loarger then 200 char without the extension
		if((strImageFileName.Replace(strImageFileExtension,"")).Length > 200)
			//shorts the file name to fit into the database
			strImageFileName = (strImageFileName.Replace(strImageFileExtension,"")).Substring(0,200) + strImageFileExtension;
		
		while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(strSavePath + strImageFileName)))
		{
			file_append++;
			strImageFileName = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + strImageFileExtension.ToLower();
		}//end of while loop
		
		// Save the stream to disk
		System.IO.FileStream newFile = new System.IO.FileStream(HttpContext.Current.Server.MapPath(strSavePath + strImageFileName.Replace("'","")), System.IO.FileMode.Create);
		newFile.Write(bytData, 0, bytData.Length);
		newFile.Close();
		
		//checks if this image needs to be a thumbnail as there will be times that a Thumbnail is needed
		if(intMaxThumbnailWidth > 0 && intMaxThumbnailHeight > 0)
		{
			//ues a memory stream of the image
			using (MemoryStream ms = new MemoryStream(bytData, 0, bytData.Length))
			{
				//writes to memory stream using the bytes from the image
				ms.Write(bytData, 0, bytData.Length);
		
				//uses a image from the memory stream to recreate the image as a file
				using(SD.Image sdimgCroppedImage = SD.Image.FromStream(ms, true))
				{
					//uses the image from the uploading to make different version of them 
					//to use in different areas of the site
					using(SD.Image imgCurrent = SD.Image.FromFile(HttpContext.Current.Server.MapPath(strSavePath + strImageFileName.Replace("'",""))))
					{
						SD.Image imgThumb = resizeImage(imgCurrent, intMaxThumbnailWidth, intMaxThumbnailHeight);//holds the image as a thumbnail for later use 
										
						//saves the image to server again this time in a thumbnail of what was just upload
						imgThumb.Save(HttpContext.Current.Server.MapPath(strSavePath + strImageFileName).Replace(".","_upload_thumbnail."), sdimgCroppedImage.RawFormat);
					}//end of using
				}//end of using
			}//end of using
		}//end of if
		
		return strSavePath + strImageFileName.Replace("'","");
	}//end of uploadImage()
}//end of class General