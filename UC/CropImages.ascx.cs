// crops images

public partial class CropImages : System.Web.UI.UserControl
{
	private bool boolUseUser = false;//holds if this is a FH or user created object like obituary
	private int intMaxWidth = 546;//holds what the max of the width
	private int intMaxHeight = 546;//holds what the max of the height
	private int intDefaultWidth = 546;//holds the default of the width
	private int intDefaultHeight = 285;//holds the default of the height
	private int intMaxThumbnailWidth = 320;//holds what the max of the width for thumbnail
	private int intMaxThumbnailHeight = 320;//holds what the max of the height for thumbnail
	private int intMaxThumbnailUploadWidth = 237;//holds what the max of the width for thumbnail that is a little bit smaller
	private int intMaxThumbnailUploadHeight = 237;//holds what the max of the height for thumbnail that is a little bit smaller
	private int intMaxIconWidth = 80;//holds what the max of the width for icon
	private int intMaxIconHeight = 80;//holds what the max of the height for icon
	private string strPostBackLoc = "ctr582$FuneralHomeEditor";//holds the holds the location of the post back for lbChooseFH

	#region Properties
	
	public string HeaderText
    { 
        set { litImageRightHeader.Text = value;}
    }//end of HeaderText
	
	public int DefaultHeight 
    { 
        get { return intDefaultHeight; } 
        set { 
		//checks if the intDefaultHeight is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intDefaultHeight = 1;
		else
			intDefaultHeight = value;
		}
    }//end of DefaultHeight
	
	public int DefaultWidth 
    { 
        get { return intDefaultWidth; } 
        set { 
		//checks if the intDefaultWidth is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intDefaultWidth = 1;
		else
			intDefaultWidth = value;
		}
    }//end of DefaultWidth
		
	public int MaxHeight 
    { 
        get { return intMaxHeight; } 
        set { 
		//checks if the intMaxHeight is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxHeight = 1;
		else
			intMaxHeight = value;
		}
    }//end of MaxHeight
	
	public int MaxIconHeight 
    { 
        get { return intMaxIconHeight; } 
        set { 
		//checks if the intIconMax is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxIconHeight = 1;
		else
			intMaxIconHeight = value;
		}
    }//end of MaxIconHeight
	
	public int MaxIconWidth 
    { 
        get { return intMaxIconWidth; } 
        set { 
		//checks if the intIconMax is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxIconWidth = 1;
		else
			intMaxIconWidth = value;
		}
    }//end of MaxIconWidth
	
	public int MaxImage 
    { 
        get { return Convert.ToInt32(hfMaxImage.Value); } 
        set { 
			//checks if the intMax is lesser then or same as zero
			//if so then default it to 1
			if(value <= 0)
				hfMaxImage.Value = "1";
			else
				hfMaxImage.Value = Convert.ToString(value);
		}
    }//end of MaxImage
	
	public int MaxWidth 
    { 
        get { return intMaxWidth; } 
        set { 
		//checks if the intMaxWidth is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxWidth = 1;
		else
			intMaxWidth = value;
		}
    }//end of MaxWidth
	
	public int MaxThumbnailHeight 
    { 
        get { return intMaxThumbnailHeight; } 
        set { 
		//checks if the intMaxThumbnailHeight is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxThumbnailHeight = 1;
		else
			intMaxThumbnailHeight = value;
		}
    }//end of MaxThumbnailHeight
	
	public int MaxThumbnailWidth 
    { 
        get { return intMaxThumbnailWidth; } 
        set { 
		//checks if the intMaxThumbnailWidth is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxThumbnailWidth = 1;
		else
			intMaxThumbnailWidth = value;
		}
    }//end of MaxThumbnailWidth
	
	public int MaxThumbnailUploadHeight 
    { 
        get { return intMaxThumbnailUploadHeight; } 
        set { 
		//checks if the intMaxThumbnailUploadHeight is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxThumbnailUploadHeight = 1;
		else
			intMaxThumbnailUploadHeight = value;
		}
    }//end of MaxThumbnailUploadHeight
	
	public int MaxThumbnailUploadWidth 
    { 
        get { return intMaxThumbnailUploadWidth; } 
        set { 
		//checks if the intMaxThumbnailUploadWidth is lesser then or same as zero
		//if so then default it to 1
		if(value <= 0)
			intMaxThumbnailUploadWidth = 1;
		else
			intMaxThumbnailUploadWidth = value;
		}
    }//end of MaxThumbnailUploadWidth
			
	public string PostBackLoc
    { 
        get { return strPostBackLoc; } 
        set { strPostBackLoc = value; }
    }//end of PostBackLoc
	
	public int setFHID
    {
        set { hfCurrentID.Value = Convert.ToString(value);}
    }//end of setFHID
	
	public int setFHIDDir
    {
        set { hfCurrentIDDir.Value = Convert.ToString(value);}
    }//end of setFHIDDir
		
	public string setDir
    { 
        set { hfDir.Value = value;}
    }//end of setDir
		
	public bool useUser 
    { 
        get { return boolUseUser; } 
        set { boolUseUser = value;}
    }//end of useUser

    #endregion
	
	#region Private Funcation
	
	//crop an image base on what the user has selected using jCrop
	private byte[] cropImage(string strImageName, int intImageWidth, int intImageHeight, int intImageX, int intImageY)
	{
		try
	  	{
			using (SD.Image OriginalImage = SD.Image.FromFile(strImageName))
			{
				int intNewImageHeight = intImageHeight;//holds the height of the new image that will be create out of the crop old image
				int intNewImageWidth = intImageWidth;//holds the width of the new image that will be create out of the crop old image
				int intNewImageYStart = 0;//holds the Y coordinate to center the image at the start
				int intNewImageXStart = 0;//holds the X coordinate to center the image at the start

				//checks if the intNewImageWidth is not intDefaultWidth
				if(intNewImageWidth != intDefaultWidth)
				{
					//does the calucations for the center the image for the X coordinate
					intNewImageXStart = ((intDefaultWidth - intNewImageWidth) / 2);
				
					//default intNewImageWidth to the max width allowed in order to created bars on the side
					intNewImageWidth = intDefaultWidth;
				}//end of if
					
				//checks if the intNewImageHeight is not intDefaultHeight
				if(intNewImageHeight != intDefaultHeight)
				{
					//does the calucations for the center the image for the Y coordinate
					intNewImageYStart = ((intDefaultHeight - intNewImageHeight) / 2);
					
					//default intNewImageHeight to the max height allowed in order to created bars
					intNewImageHeight = intDefaultHeight;
				}//end of if
				
		  		using (SD.Bitmap bmp = new SD.Bitmap(intNewImageWidth, intNewImageHeight))
		  		{					
					bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
										
					using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
					{
			  			Graphic.SmoothingMode = SmoothingMode.AntiAlias;
			  			Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			  			Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			  			Graphic.DrawImage(OriginalImage, new SD.Rectangle(intNewImageXStart, intNewImageYStart,  intImageWidth, intImageHeight), intImageX, intImageY, intImageWidth, intImageHeight, SD.GraphicsUnit.Pixel);
			  			MemoryStream ms = new MemoryStream();
						
			  			bmp.Save(ms, OriginalImage.RawFormat);
			  			
						return ms.GetBuffer();
					}//end of using
		  		}//end of using
			}//end of using
	  	}//end of try
	  	catch (Exception ex)
	  	{
			throw (ex);
	  	}//end of catch
	}//end of cropImage()
	
	//resets the Croping section
	private void resetCroping()
	{
		try
		{			
			//resets lblCurrentStep and litHowToUse
			lblCurrentStep.Text = "Click ‘Add New’ to begin uploading your photos.";
			litHowToUse.Text = "";
			
			//resets the breadcrumb
			panBreadcrumbUpload.CssClass = panBreadcrumbUpload.CssClass.Replace(" divSelectedBreadcrumb", "");
			panBreadcrumbCrop.CssClass = panBreadcrumbCrop.CssClass.Replace(" divSelectedBreadcrumb", "");
			panBreadcrumbResize.CssClass = panBreadcrumbResize.CssClass.Replace(" divSelectedBreadcrumb", "");
			panBreadcrumbUpload.CssClass += " divSelectedBreadcrumb";
			
			//turns back on the add for the next time the user wants to add in a image
			cmdImageAddNew.Visible = true;
			
			//turns off panImageAddNew and the cropping area also renenables the uplaod for the next image
			panImageAddNew.Visible = false;
			panResize.Visible = false;
			panCrop.Visible = false;
			panUpload.Visible = true;
			
			//resets the W, H, X, Y area for the next upload image
			W.Value = "";
			H.Value = ""; 
			X.Value = "";
			Y.Value = "";
			
			try
			{
				//checks if the temp directory exists
				if (Directory.Exists(Server.MapPath(".\\images\\" + hfDir.Value + "\\Temp\\" + hfCurrentIDDir.Value)))
				{
					DirectoryInfo diTemp = new DirectoryInfo(Server.MapPath(".\\images\\" + hfDir.Value + "\\Temp\\" + hfCurrentIDDir.Value));//holds the temporay dirtory where all of the temp images are located
					
					//deletes the temp directory
					diTemp.Delete(true);
				}//end of if
			}//end of try
	  		catch (Exception)
	  		{
				//the temp directory is currently in used however next time the user comes around it will be removed
		  	}//end of catch
		}//end of try
	  	catch (Exception ex)
	  	{
			Response.Write("Reset Crop Error: " + ex.Message);// + " " + ex.StackTrace;
	  	}//end of catch
	}//end of resetCroping()
	
	#endregion
	
	#region Public Funcations
	
	//gets the images in order to send them to the user
	public string[] getImages()
	{		
		//returns the images that are in this version of CropImages
		return hfCurrentImages.Value.Split(new string[] {"@*"}, StringSplitOptions.RemoveEmptyEntries);
	}//end of getImages()
	
	//loads the images into the display
	public bool loadImages(bool boolResetCroping = true)
	{
		try
		{
			DataTable dtImage = DAL.getRow("", "Where  = " + hfCurrentID.Value + " Order by ");//holds all of the images in order
			int intRowImageIndex = 0;//holds the contorl to see how many images are allow in a row
			int intRowImageIndexID = 1;//holds the ids of the images that are going to be display
			int intDatabaseImageID = 0;//holds the Image ID coming from the database
			string strDatabaseImageFileName = "";//holds the Image File Name coming from the database
			string strDatabaseImageFileNameAlt = "";//holds the Image File Name Alt coming from the database
			string strDraftDir = "Draft";//holds the location of where the draft images are going
			
			//checks if this is from FH or User
			if(boolUseUser == true)
				//gets the details of the image
				dtImage = DAL.getRow("","Where  = " + hfCurrentID.Value + " Order by ");

			//sets the ajax which table to save the order to 
			litSortCropImage.Text = "sendSortFHImage('/ASP/SortFHImage.aspx', 'divMessage', getDocID('" + hfCurrentImages.ClientID + "'), " + hfCurrentID.Value + ", " + boolUseUser.ToString().ToLower() + ");";
						
			//checks if is is the live if so then draft dirtory is not need
			if(hfCurrentIDDir.Value == hfCurrentID.Value)
				strDraftDir = "";
						
			//resets litImage and hfCurrentImages for in order to display a fresh
			litImage.Text = "";
			hfCurrentImages.Value = "";
						
			//checks if the number of rows is leaster then the maxium number of images allowed
			if (dtImage.Rows.Count < Convert.ToInt32(hfMaxImage.Value))
				//displays the max number of images allowed
				litMaxImage.Text = "<label>Upload up to </label><label id='lblCropImagesLeft'>" + (Convert.ToInt32(hfMaxImage.Value) - dtImage.Rows.Count) + "</label><label> more images</label>";
			else
				//turns off litMaxImage as the user has hit the max
				litMaxImage.Text = "&nbsp;";
				
			//checks if the number of rows is leaster then the maxium number of images allowed
			if (dtImage.Rows.Count < Convert.ToInt32(hfMaxImage.Value))
				//turns on the add new section it uses class for the delete javascript
				//to add/or remove it when needed
				panImageAddNewContainer.CssClass = "divImageAddNew";
			else
				//turns off the add new section it uses class for the delete javascript
				//to add/or remove it when needed
				panImageAddNewContainer.CssClass = "divImageAddNew divJustHidden";
			
			//checks if dtImage has no images
			if (dtImage.Rows.Count == 0)
				//do not display the Image Timeline
				panImageMain.Visible = false;
			else
			{
				//checks if this is from FH or User
				if(boolUseUser == true)
					//displays what is the Transition Mode for the Obituary Image Timeline
					ddlTransitionModes.SelectedValue = dtImage.Rows[0]["ObituaryImageTransitionMode"].ToString();					
				else
					//displays what is the Transition Mode for the FH Image Timeline
					ddlTransitionModes.SelectedValue = dtImage.Rows[0]["FuneralHomeImageTransitionMode"].ToString();
				
				//display the Image Timeline
				panImageMain.Visible = true;
			}//end of if
	
			//goes around displaying the images
			foreach(DataRow drImage in dtImage.Rows)
			{
				//checks if this is from FH or User
				if(boolUseUser == true)
				{
					//sets the file name and id
					strDatabaseImageFileName = drImage["ImageName"].ToString();
					intDatabaseImageID = Convert.ToInt32(drImage["Id"].ToString());
				}//end of if
				else
				{
					//sets the file name and id
					strDatabaseImageFileName = drImage["FuneralHomeImage"].ToString();
					intDatabaseImageID = Convert.ToInt32(drImage["FuneralHomeImageID"].ToString());
				}//end of else
				
				//checks if the file in file system if so then display it
				if (File.Exists(Server.MapPath("~\\images\\" + hfDir.Value + "\\" + hfCurrentIDDir.Value + "\\" + strDraftDir + "\\" + strDatabaseImageFileName)))
				{					
					//adds this image to be display
					litImage.Text += "<div id='divImage" + intRowImageIndexID + "' class='customLeft divFHImageMapDisplayLeft'>" + 
						"<div class='divFHDisplayImage'>" + 
							"<div class='divFHDisplayDelete'>" + 
								"<a href='javascript:void(0);' onClick='if(confirm(&quot;Do you what to remove image?&quot;) == true){sendDeleteFHImage(&quot;/ASP/DeleteFHImage.aspx&quot;, &quot;divImageMessage&quot;, getDocID(&quot;divImageMainContainer&quot;), getDocID(&quot;divImage" + intRowImageIndexID + "&quot;), getDocID(&quot;" + hfCurrentImages.ClientID + "&quot;), getDocID(&quot;" + panImageMain.ClientID + "&quot;), getDocID(&quot;" + panImageAddNewContainer.ClientID + "&quot;), &quot;" + strDatabaseImageFileName + "&quot;, " + intDatabaseImageID + ", " + hfCurrentID.Value + ", " + hfCurrentIDDir.Value + ", &quot;" + hfDir.Value + "&quot;,&quot;" + strPostBackLoc + "&quot;," + Convert.ToInt32(hfMaxImage.Value) + ", getDocID(&quot;lblCropImagesLeft&quot;), getDocID(&quot;divNumberMoreImage&quot;));}'>X</a>" + 
							"</div>" + 
							"<img alt='" + strDatabaseImageFileNameAlt + " Image " + intRowImageIndexID + "' src='/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + "/" + strDatabaseImageFileName + "' />" +
						"</div>" + 
					"</div>";
					
					//updates the hfCurrentImages as this is what is used to keep track of what order the
					//images are in when the user moves them around
					hfCurrentImages.Value += "@*" + strDatabaseImageFileName;
										
					//checks if intRowImageIndex is above 3 if so then reset the line for the next row
					if(intRowImageIndex >= 2)
					{					
						//enables the footer to be display for this row
						//litImage.Text += "<div class='customFooter divFHImageMapDisplayFooter'></div>";
										
						//resets the row image index for the next line
						intRowImageIndex = 0;
					}//end of if
					else
						intRowImageIndex++;
						
					intRowImageIndexID++;
				}//end of if
			}//end of foreach
			
			//for the footer to be display for this row
			litImage.Text += "<div class='customFooter divFHImageMapDisplayFooter'></div>";
			
			//checks if the croping/image area should be reseted
			if(boolResetCroping == true)
				resetCroping();
				
			return true;
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;// + " " + ex.StackTrace;
			lblError.Visible = true;
			
			return false;
		}//end of catch
	}//end of loadImages()
		
	#endregion
	
	protected void Page_PreRender(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
			//resets the Croping section for any post back as there
			//maybe errors with the croping or deleting images
			loadImages(false);
        }//end of if
	}//end of Page_PreRender()
	
	protected void cmdUpload_Click(object sender, EventArgs e)
	{
		try
		{
			//Change if they what to changes the Main Image of the Category
			if (!string.IsNullOrEmpty(fulImageUpload.PostedFile.FileName)) 
			{
				string strImageLocDir = "/images/" + hfDir.Value + "/Temp/" + hfCurrentIDDir.Value + "/";//holds the loction
				
				//checks if strImageLocDir temp dirtory has this item in it if not then create one
				if(!Directory.Exists(Server.MapPath("~/" + strImageLocDir)))
					//creates dirtory for where the image will be store temporay
					Directory.CreateDirectory(Server.MapPath("~/" + strImageLocDir));
					
				//checks if strImageLocDir draft dirtory has this item in it if not then create one
				if(!Directory.Exists(Server.MapPath("~/" + strImageLocDir.Replace("/Temp/","/") + "Draft/")))
					//creates dirtory for where the image will be store
					Directory.CreateDirectory(Server.MapPath("~/" + strImageLocDir.Replace("/Temp/","/") + "Draft/"));
					
				//checks if strImageLocDir dirtory has this item in it if not then create one
				if(!Directory.Exists(Server.MapPath("~/" + strImageLocDir.Replace("/Temp/","/"))))
					//creates dirtory for where the image will be store
					Directory.CreateDirectory(Server.MapPath("~/" + strImageLocDir.Replace("/Temp/","/")));
				
				//uplaods the Image to the site
				string strImageName = General.uploadImage(strImageLocDir,fulImageUpload.PostedFile);
				
				//checks if there was an error with the upliad
				if(strImageName.IndexOf("ERROR! ") >= 0)
				{
					//tells the user that there was an error
					lblUploadError.Text = "Image Upload Error: " + strImageName;
					lblUploadError.Visible = true;
				}//end of if
				else
				{
					//uses the image 
					using(SD.Image imgCurrent = SD.Image.FromFile(Server.MapPath("~" + strImageName)))
					{
						SD.Image imgThumb = General.resizeImage(imgCurrent, intMaxWidth, intMaxHeight);//holds the image as a thumbnail
						
						//saves the image to server again this time in a more small image
						imgThumb.Save(Server.MapPath("~" + strImageName.Replace(" ","").Replace(".","_thumb2.")));
						
						//changes the breadcrumb from upload to resizing
						panBreadcrumbUpload.CssClass = panBreadcrumbUpload.CssClass.Replace("divSelectedBreadcrumb", "");
						panBreadcrumbResize.CssClass += " divSelectedBreadcrumb";
																												
						//turns off the upload and gives the user resizing
						panUpload.Visible = false;
						panResize.Visible = true;
						imgResize.ImageUrl = strImageName.Replace(" ","").Replace(".","_thumb2.");
						imgResize.Width = imgThumb.Width;
						imgResize.Height = imgThumb.Height;
						
						//sets the width and height orginal outline for the user to tell where is the starting point
						panUploadResizeOrginalArea.Attributes.Add("style", "width: " + imgThumb.Width + "px;height: " + imgThumb.Height + "px;");
						
						//sets the size of the image into WResize, HResize area as a starting point for the resizing
						WResize.Value = imgThumb.Width.ToString();
						HResize.Value = imgThumb.Height.ToString();
						
						lblCurrentStep.Text = "Resize your photo";
						litHowToUse.Text = "<br/><label>Resize your photo by clicking and dragging the lower right corner of the picture, making the image larger or smaller. The red 'represents' the height and width the final image will be displayed at. Click the resize button when finished. During the next step, this 'crop box' can be moved freely around the image to select the area you wish to display.</label>";
					}//end of using
				}//end of else
			}//end of if
		}//end of try
		catch (Exception ex)
		{			
			lblUploadError.Text = "Upload Error: " + ex.Message;// + " " + ex.StackTrace
			lblUploadError.Visible = true;
		}//end of catch
	}//end of cmdUpload_Click()
	
	protected void cmdResize_Click(object sender, EventArgs e)
	{	
		try
		{
			//turns off the error message
			lblUploadResizeError.Visible = false;

			//checks if there cropping image as the user may refresh the page this will active it
			if(File.Exists(Server.MapPath("~" + imgResize.ImageUrl)))
			{
				//uses the image 
				using(SD.Image imgCurrent = SD.Image.FromFile(Server.MapPath("~" + imgResize.ImageUrl)))
				{
					string strWidthResize = WResize.Value;//holds the width value that will be the new size
					string strHeightResize = HResize.Value;//holds the height value that will be the new size
					int intAllowedImageHeight = intDefaultHeight;//holds the height of the crop area alloed
					int intAllowedImageWidth = intDefaultWidth;//holds the width of the crop area alloed
										
					//checks if there is a . in strWidthResize 
					if(strWidthResize.IndexOf(".") > 0)
						//removes all number from the the . on as this couses an error and for some reason
						//the convert does not work just rounds up to the nears number
						strWidthResize = strWidthResize.Substring(0,strWidthResize.IndexOf("."));
						
					//checks if there is a . in strHeightResize 
					if(strHeightResize.IndexOf(".") > 0)
						//removes all number from the the . on as this couses an error and for some reason
						//the convert does not work just rounds up to the nears number
						strHeightResize = strHeightResize.Substring(0, strHeightResize.IndexOf("."));
					
					//checks if strWidthResize is smaill of then the default width if so then sent the croping 
					//to that width as to allowed a for bars on the sides 
					//if the image is smaller then the default width
					if(Convert.ToInt32(strWidthResize) < intDefaultWidth)
						intAllowedImageWidth = Convert.ToInt32(strWidthResize);
						
					//checks if strHeightResize is smaill of then the default height 
					//if so then sent the croping to that height as to allowed 
					//a for bars on the sides if the image is smaller then the default height
					if(Convert.ToInt32(strHeightResize) < intDefaultHeight)
						intAllowedImageHeight = Convert.ToInt32(strHeightResize);
					
					SD.Image imgThumb = General.resizeImage(imgCurrent, Convert.ToInt32(strWidthResize), Convert.ToInt32(strHeightResize));//holds the image as a new size that the user likes
					
					//saves the image to server again this time in a more small image
					imgThumb.Save(Server.MapPath("~" + imgResize.ImageUrl.Replace("_thumb2.","_thumb.")));

					//changes the breadcrumb from resizing to cropping
					panBreadcrumbResize.CssClass = panBreadcrumbResize.CssClass.Replace("divSelectedBreadcrumb", "");
					panBreadcrumbCrop.CssClass += " divSelectedBreadcrumb";

					//turns off the resize and gives the user croping
					panResize.Visible = false;
					panCrop.Visible = true;
					imgCrop.ImageUrl = imgResize.ImageUrl.Replace("_thumb2.","_thumb.");
					
					//sets jquery for croping with all as there will be
					litCrop.Text = "$('#" + imgCrop.ClientID + "').Jcrop({\n" + 
						"onSelect: storeCoords,\n" + 
						"minSize: [ " + intAllowedImageWidth + ", " + intAllowedImageHeight + "],\n" + 
						"maxSize: [ " + intAllowedImageWidth + ", " + intAllowedImageHeight + "],\n" + 
						"setSelect: [ " + intAllowedImageWidth + ", " + intAllowedImageHeight + ", 0, 0 ]\n" + 
					"});";
					
					//sets jquery for image of the croping with all as there will be
					litCropImageClick.Text = "$('#" + imgCrop.ClientID + "').click(function() {\n" + 
						litCrop.Text + "\n" + 
					"});";
					
					//sets the size of the image into W, H, X, Y area as a starting point for the cropping
					W.Value = imgThumb.Width.ToString();
					H.Value = imgThumb.Height.ToString();
					X.Value = "0";
					Y.Value = "0";
					
					//sets the text for the Croping
					lblCurrentStep.Text = "Crop your photo";
					litHowToUse.Text = "<br/><label>Click and position the highlighted box to the desired area of your photo and then click the CROP button to finalize your selection. If your image is less than the display height or width, the crop box will not be visible. Click CROP to continue.</label>";
				}//end of using
			}//end of if
		}//end of try
		catch (Exception ex)
		{			
			lblUploadResizeError.Text = ex.Message;// + " " + ex.StackTrace;
			lblUploadResizeError.Visible = true;
		}//end of catch
	}//end of cmdResize_Click()
	
	protected void cmdCrop_Click(object sender, EventArgs e)
	{	
		try
		{
			//turns off the error message
			lblUploadCropError.Visible = false;

			//checks if there cropping image as the user may refresh the page this will active it
			if(File.Exists(Server.MapPath("~" + imgCrop.ImageUrl)))
			{
				byte[] bytCropImage = cropImage(Server.MapPath("~" + imgCrop.ImageUrl), Convert.ToInt32(W.Value), Convert.ToInt32(H.Value), Convert.ToInt32(X.Value), Convert.ToInt32(Y.Value));//holds the byties of the crop image
			  
			  	//ues a memory stream of the image
				using (MemoryStream ms = new MemoryStream(bytCropImage, 0, bytCropImage.Length))
				{
					//writes to memory stream using the bytes from from the image
					ms.Write(bytCropImage, 0, bytCropImage.Length);
			
					//uses a image from the memory stream to recreate the image as a file
					using(SD.Image sdimgCroppedImage = SD.Image.FromStream(ms, true))
					{
						// Make sure a duplicate file doesn't exist.  If it does, keep on appending an 
						// incremental numeric until it is unique
						string strImageFile = Server.MapPath("~" + imgCrop.ImageUrl.Replace("/images/" + hfDir.Value + "/Temp/" + hfCurrentIDDir.Value + "/","/"));//holds the location of the file
						string strImageFileLoc = System.IO.Path.GetFileName(strImageFile);//holds the name of the file
						string strDraftDir = "Draft/";//holds the location of where the draft images are going
						int intFileAppend = 0;//holds which a number to make the file unqiure
						
						//checks if is is the live if so then draft dirtory is not need
						if(hfCurrentIDDir.Value == hfCurrentID.Value)
							strDraftDir = "";
											
						//goes around checking if there is already a file with the same name 
						while (System.IO.File.Exists(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/"  + strDraftDir + strImageFileLoc)))
						{
							//adds to the file append in order to make this file unqiue in the dirtory
							intFileAppend++;
							strImageFileLoc = System.IO.Path.GetFileNameWithoutExtension(strImageFile) + intFileAppend.ToString() + System.IO.Path.GetExtension(strImageFile).ToLower();
						}//end of while loop
						
						//actully crops the image
						sdimgCroppedImage.Save(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strImageFileLoc), sdimgCroppedImage.RawFormat);
						
						//uses the image  for the 
						using(SD.Image imgCurrent = SD.Image.FromFile(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strImageFileLoc)))
						{
							SD.Image imgLarge = General.resizeImage(imgCurrent, (intMaxWidth * 2), (intMaxHeight * 2));//holds the image as a larger image for imgCurrent for panning effect
							SD.Image imgThumb = General.resizeImage(imgCurrent, intMaxThumbnailWidth, intMaxThumbnailHeight);//holds the image as a thumbnail for later use
							SD.Image imgThumbUpload = General.resizeImage(imgCurrent, intMaxThumbnailUploadWidth, intMaxThumbnailUploadHeight);//holds the image as a thumbnail that is a little bit smaller as parts of the site need to use this
							SD.Image imgIcon = General.resizeImage(imgCurrent, intMaxIconWidth, intMaxIconHeight);//holds the image as a icon for imgCurrent that will go into the navigation of the image slider
						
							//saves the image to server again this time in a more larger image
							imgLarge.Save(Server.MapPath("~/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strImageFileLoc).Replace(".","_LG."), sdimgCroppedImage.RawFormat);
						
							//saves the image to server again this time in a more small image
							imgThumb.Save(Server.MapPath("~/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strImageFileLoc).Replace(".","_thumbnail."), sdimgCroppedImage.RawFormat);
							
							//saves the image to server again this time in a more small image
							imgThumbUpload.Save(Server.MapPath("~/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strImageFileLoc).Replace(".","_upload_thumbnail."), sdimgCroppedImage.RawFormat);
													
							//saves the image to server again this time in a more smaller image
							imgIcon.Save(Server.MapPath("~/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strImageFileLoc).Replace(".","_icon."), sdimgCroppedImage.RawFormat);
																				
							//checks if this is FH Image or a User Image
							if(boolUseUser == true)
							{
								DataTable dtImage = DAL.getRow("", "Where  = " + hfCurrentID.Value);//holds all of the images for this Obituary in order to get the total number of images for this ese
													
								//updates the Obituary with the newest image
								DAL.addUpdateObituaryImage(0, Convert.ToInt32(hfCurrentID.Value), (dtImage.Rows.Count + 1), strImageFileLoc.Replace("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/",""));
							}//end of if
							else
							{
								DataTable dtImage = DAL.getRow("", "Where  = " + hfCurrentID.Value);//holds all of the images for this FH in order to get the total number of images for this ese
								
								//updates the FuneralHome with the newest image
								DAL.addUpdateFuneralHomeImage(0, Convert.ToInt32(hfCurrentID.Value), (dtImage.Rows.Count + 1), strImageFileLoc.Replace("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/",""), true);
							}//end of else
							
							//updates all images again with the current transition mode that was selected
							ddlTransitionModes_SelectedIndexChanged(sender, e);
						}//end of using
					}//end of using
					
					//closes the stream
					ms.Close();
				}//end of using
			}//end of if
				
			//resets the Croping/Image section
			loadImages();
		}//end of try
		catch (Exception ex)
		{			
			lblUploadCropError.Text = ex.Message;// + " " + ex.StackTrace;
			lblUploadCropError.Visible = true;
		}//end of catch
	}//end of cmdCrop_Click()
	
	protected void cmdCropCancel_Click(object sender, EventArgs e)
	{
		//resets the Croping section
		loadImages();
	}//end of cmdCropCancel_Click()
	
	protected void cmdImageAddNew_Click(object sender, EventArgs e)
	{
		//turns off the error messages
		lblUploadCropError.Visible = false;
		lblUploadError.Visible = false;
		lblUploadResizeError.Visible = false;
		
		//turns on the Add New Section and turns off the button 
		panImageAddNew.Visible = true;
		
		//because the user can remove a image in the middle it is best to remove the add new 
		//button then check if there is at least one image that has not been taken yet
		cmdImageAddNew.Visible = false;
		
		//sets the text for the Browse and Upload
		lblCurrentStep.Text = "Depending on your browser, click the BROWSE or CHOOSE FILE button. A window will open allowing you to select your photo - then click UPLOAD. Please note, for optimal results use images of approximately 2048px (pixels) in width and under 3MB in size.";
		litHowToUse.Text = "";
	}//end of cmdImageAddNew_Click()
	
	protected void ddlTransitionModes_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			//updates the Transition Mode for either FH or User
			DAL.updateFHObituaryImage(Convert.ToInt32(boolUseUser), Convert.ToInt32(hfCurrentID.Value), Convert.ToInt32(ddlTransitionModes.SelectedValue));
		}//end of try
		catch (Exception ex)
		{
			lblUploadCropError.Text = ex.Message;// + " " + ex.StackTrace;
			lblUploadCropError.Visible = true;
		}//end of catch
	}//end of ddlTransitionModes_SelectedIndexChanged()
}//end of User Contorl