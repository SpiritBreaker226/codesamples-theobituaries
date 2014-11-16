// funeral banner rotation banner

public partial class FHImageSlider : System.Web.UI.UserControl
{
	private bool boolUseUser = false;//holds if this is a FH or user created object like obituary
	
	#region properties
	
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
	
	public bool setSliderImage
    { 
        set { panSliderImage.Visible = value;}
    }//end of setSliderImage
		
	public bool useUser 
    { 
        get { return boolUseUser; } 
        set { boolUseUser = value;}
    }//end of useUser
	
	#endregion
				
	protected void Page_PreRender(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{									
			DataTable dtImage = DAL.getRow("", "Where  = " + hfCurrentID.Value + " Order by ");//holds all of the images in order
			int intRowID = 0;//holds the unqiure row id
			int intDatabaseImageOrder = 0;//holds the Image Order coming from the database
			int intDatabaseImageTransitionMode = 0;//holds the Image Transition Mode
			string strDatabaseImageFileName = "";//holds the Image File Name coming from the database
			string strDraftDir = "Draft/";//holds the location of where the draft images are going
			
			//checks if is is the live if so then draft dirtory is not need
			if(hfCurrentIDDir.Value == hfCurrentID.Value)
				strDraftDir = "";
			
			//checks if this is from FH or User
			if(boolUseUser == true)
				//gets the details of the image
				dtImage = DAL.getRow("","Where  = " + hfCurrentID.Value + " Order by ");
				
			//checks if there is a image to display
			if(dtImage.Rows.Count > 0)
			{					
				//goes around adding the images
				foreach (DataRow drImage in dtImage.Rows)
				{
					//checks if this is from FH or User
					if(boolUseUser == true)
					{
						//sets the file name and id
						strDatabaseImageFileName = drImage[""].ToString();
						intDatabaseImageOrder = Convert.ToInt32(drImage[""].ToString());
						intDatabaseImageTransitionMode = Convert.ToInt32(drImage[""].ToString());
					}//end of if
					else
					{
						//sets the file name and id
						strDatabaseImageFileName = drImage[""].ToString();
						intDatabaseImageOrder = Convert.ToInt32(drImage[""].ToString());
						intDatabaseImageTransitionMode = Convert.ToInt32(drImage[""].ToString());
					}//end of else
					
					//checks that this is first item as this is the only needs to happen onnce
					//also checks if there is at least two images to transition to
					//as the image would transiton to nothing if it was one image
					if(intRowID == 0 && dtImage.Rows.Count > 1)
					{
						//sets the scripting for the image sliding effect
						litScript.Text = "<script type='text/javascript'>\n" + 
							"$(document).ready(function () {\n" + 
								"$('#" + panSliderImage.ClientID + "').bannerscollection_zoominout({\n" + 
									"skin: 'opportune',\n" + 
									"responsive: true,\n" + 
									"duration: 8,\n" + 
									"durationIEfix: 8,\n" + 
									"autoPlay: 8,\n";
									
									//checks if this is Transition Mode as it uses a different transition effect to 
									//move to another slide
									if(intDatabaseImageTransitionMode == 2)
										litScript.Text += "fadeSlides: false,\n";
									
						litScript.Text += "width: 543,\n" + 
									"height: 285,\n" + 
									"circleRadius: 8,\n" + 
									"circleLineWidth: 4,\n" + 
									"circleColor: '#ffffff',\n" + 
									"circleAlpha: 50,\n" + 
									"behindCircleColor: '#000000',\n" + 
									"behindCircleAlpha: 20,\n" + 
									"showCircleTimer: false,\n" + 
									"showNavArrows: false,\n" + 
									"thumbsWrapperMarginTop: 30\n" + 
								"});\n" + 
							"});\n" + 
						"</script>";
					}//end of if
					else if(intRowID == 0)
					{
						//sets the scripting for the one image display
						litScript.Text = "<script type='text/javascript'>\n" + 
							"$(document).ready(function () {\n" +
								"$('.myloader').css('display','none');\n" + 
								"$('.bannerscollection_zoominout_list').css('display','block');\n" + 
							"});\n" + 
						"</script>";
					}//end of else
									
					//checks if the file in file system if so then display it
					if (File.Exists(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/"  + strDraftDir + strDatabaseImageFileName)))
					{
						//uses the image 
						using(SD.Image imgCurrent = SD.Image.FromFile(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/"  + strDraftDir + strDatabaseImageFileName)))
						{
							litSliderImage.Text += "<li id='liSlider" + intRowID + "' ";
							
							//checks if there is at least two images to transition to
							//as the image would transiton to nothing if it was one image
							if(dtImage.Rows.Count > 1)
							{
								//checks if there is thumbnail
								if (File.Exists(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/"  + strDraftDir + strDatabaseImageFileName.Replace(".","_icon."))))
									//uses the icon for the thumbnail nav button 
									litSliderImage.Text += " data-bottom-thumb='/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strDatabaseImageFileName.Replace(".","_icon.") + "'";
								else
									//uses the thumbnail for the thumbnail nav button
									litSliderImage.Text += " data-bottom-thumb='/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir + strDatabaseImageFileName.Replace(".","_thumbnail.") + "'";
								
								//checks which transition mode the image are in panning, fading, sweeping
								if(intDatabaseImageTransitionMode == 0)
								{
									System.Random ranNumber = new System.Random((int)System.DateTime.Now.Ticks);//holds the object that will random gen numbers
									string[] strStartingVerticalPosition = new string[2] {"bottom", "top"};//holds the vertical position where the image will start
									string[] strStartingHorizontalPosition = new string[2] {"left", "right"};//holds the horizontal position where the image will start
									
									//sets the transition mode to Panning and randomly choose a vertical and horizontal position
									litSliderImage.Text += " data-horizontalPosition='" + strStartingHorizontalPosition[ranNumber.Next(0, 2)] + "' data-verticalPosition='" + strStartingVerticalPosition[ranNumber.Next(0, 2)] + "'  data-initialZoom='0.72' data-finalZoom='1'";
								}//end of if
								else if(intDatabaseImageTransitionMode == 1)
									//sets the transition mode to Fading
									litSliderImage.Text += " data-initialZoom='1' data-finalZoom='1' data-transition='fade'";
								else
									//sets the transition mode to Sweeping
                                    litSliderImage.Text += " data-initialZoom='1' data-finalZoom='1'";
							}//end of if
							else
								//sets the this itme for displaying in one image mode
								litSliderImage.Text += " class='liImageSliderOneImage'";
								
							litSliderImage.Text += ">" + 
								"<img src=\"/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/" + strDraftDir;
								
							//checks if this is a panning TransitionMode and there is a larger version of the 
							//image as panning needs a bigger verion to move around in
							if(intDatabaseImageTransitionMode == 0 && File.Exists(Server.MapPath("/images/" + hfDir.Value + "/" + hfCurrentIDDir.Value + "/"  + strDraftDir + strDatabaseImageFileName.Replace(".","_LG."))))							
								litSliderImage.Text += strDatabaseImageFileName.Replace(".","_LG.") + "\" width='" + (imgCurrent.Width + 210) + "' height='" + (imgCurrent.Height + 210) + "'";
							else
								//displays the normal version
								litSliderImage.Text += strDatabaseImageFileName + "\" width='" + imgCurrent.Width + "' height='" + imgCurrent.Height + "'";
							
							litSliderImage.Text += " alt='Image " + intDatabaseImageOrder + "' id='imgSlider" + intRowID + "' />" + 
							"</li>";
						}//end of using
					}//end of if
					
					intRowID++;
				}//end of foreach
			}//end of if
			else
				//removes the slider from view
				panSliderImage.Visible = false;
		}//end of if
    }//end of Page_PreRender()
}//end of User Contorl