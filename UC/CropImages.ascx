<%@ Control Language="C#" AutoEventWireup="True" CodeFile="CropImages.ascx.cs" Inherits="CropImages" %>

<script src="https://code.jquery.com/jquery-1.9.1.js"></script>
<script src="https://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
<script src="/Portals/_default/Skins/Obit/js/jquery.Jcrop.js" type="text/javascript"></script>

<link rel="stylesheet" href="https://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
<link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.6/themes/cupertino/jquery-ui.css" />
<link href="/Portals/_default/Skins/Obit/css/jquery.Jcrop.css" rel="stylesheet" type="text/css" />

<div id="divGrayCropImageBG" class="divBasicHiddlenBackground"></div>
    	    
<asp:HiddenField ID="hfMaxImage" runat="server" Value="10" />
<asp:HiddenField ID="hfCurrentID" runat="server" Value="0" />
<asp:HiddenField ID="hfCurrentIDDir" runat="server" Value="0" />
<asp:HiddenField ID="hfDir" runat="server" Value="FH" />

<div class="divError">    
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    
    <div class="divBasicMessage" id="divMessage"></div>
</div>

<div class="divCropImageLoading divJustHidden" id="divProgress">
	<img alt="Loading" src="/Portals/_default/skins/Obit/Images/LoadingBar.gif" />
    <br/><br/>
    <label class="lblCropProgress">Loading, Please Wait!</label>
</div>

<asp:Panel runat="server" ID="panImageAddNewContainer" CssClass="divImageAddNew">
    <div id="divCropImageHowTo" class="OB-level2-introtext">
        <asp:Label runat="server" ID="lblCurrentStep" CssClass="lblCropImageHowTo" Text="Step 1 - Click 'Add New'"></asp:Label> <asp:Literal runat="server" ID="litHowToUse"></asp:Literal>
    </div>

    <asp:Button ID="cmdImageAddNew" runat="server" OnClick="cmdImageAddNew_Click" Text="Add New" CssClass="green-button" />
    
    <asp:Panel ID="panImageAddNew" runat="server" CssClass="divNewImage" Visible="false">
    	<div class="customContainer divCropBreadcrumbsContainer">
        	<asp:Panel runat="server" ID="panBreadcrumbUpload" CssClass="customLeft divCropBreadcrumbsLeft divSelectedBreadcrumb">
                <label>Step 1: Upload</label>
            </asp:Panel>
            <asp:Panel runat="server" ID="panBreadcrumbResize" CssClass="customLeft divCropBreadcrumbsLeft">
                <label>Step 2: Resize</label>
            </asp:Panel>
            <asp:Panel runat="server" ID="panBreadcrumbCrop" CssClass="customLeft divCropBreadcrumbsLeft">
                <label>Step 3: Crop</label>
            </asp:Panel>
            <div class="customFooter divCropBreadcrumbsFooter"></div>
        </div>
        
        <div class="divCropImageLoading divCropImageLoadingForCroping divJustHidden" id="divStep23Progress">
            <img alt="Loading" src="/Portals/_default/skins/Obit/Images/LoadingBar.gif" />
            <br/><br/>
            <label class="lblCropProgress">Loading, Please Wait!</label>
        </div>
        
        <asp:LinkButton ID="LinkButton1" runat="server" /> <%-- included to force __doPostBack javascript function to be rendered --%>
        
        <asp:Panel ID="panUpload" runat="server">                    
            <div class="customContainer" id="divFHImageUploadContainer">
                <div class="customLeft" id="divFHImageUploadLeft">
                    <asp:FileUpload ID="fulImageUpload" runat="server" />
                </div>
                <div class="customRight" id="divFHImageUploadRight">
					<a href="javascript:void(0);" onclick="javascript:toggleLayer('divProgress','divGrayCropImageBG','');window.setTimeout(function() {__doPostBack('<%= cmdUpload.UniqueID %>','');}, 500);" class="green-button">Upload</a>
                  	<a href="javascript:void(0);" onclick="javascript:__doPostBack('<%= cmdUploadCancel.UniqueID %>','');" class="green-button">Cancel</a>
                 	
                  	<asp:Button runat="server" ID="cmdUpload" CssClass="divJustHidden" OnClick="cmdUpload_Click" ClientIDMode="Static" />
                  	<asp:Button runat="server" ID="cmdUploadCancel" CssClass="divJustHidden" OnClick="cmdCropCancel_Click"  ClientIDMode="Static" />
                </div>
                <div class="customFooter" id="divFHImageUploadFooter"></div>
            </div>
            
            <div class="divError">
                <asp:Label runat="server" ID="lblUploadError" Visible="false"></asp:Label>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="panResize" runat="server" CssClass="divCropImage" Visible="false">
        	<div class="divUploadResizeCropArea divUploadResizeOutline"></div>
                        	
            <div class="divUploadImageArea">
                <asp:Panel runat="server" ID="panUploadResizeOrginalArea" CssClass="divUploadResizeOrginalArea divUploadResizeOutline">
                    <asp:Image ID="imgResize" runat="server" />
                </asp:Panel>
            </div>
            
            <asp:HiddenField ID="WResize" runat="server" Value="546" />
            <asp:HiddenField ID="HResize" runat="server" Value="285" />
            
            <div class="divError">
            	<asp:Label runat="server" ID="lblUploadResizeError" Visible="false"></asp:Label>	
            </div>

				<div id="divResizeButton">
            	<a href="javascript:void(0);" onclick="javascript:toggleLayer('divStep23Progress','divGrayCropImageBG','');window.setTimeout(function() {__doPostBack('<%= cmdResize.UniqueID %>','');}, 500);" class="green-button">Resize</a>
               	<a href="javascript:void(0);" onclick="javascript:__doPostBack('<%= cmdResizeCancel.UniqueID %>','');" class="green-button">Cancel</a>
            	
            	<asp:Button runat="server" ID="cmdResize" OnClick="cmdResize_Click" CssClass="divJustHidden" ClientIDMode="Static" /> 
            	<asp:Button runat="server" ID="cmdResizeCancel" OnClick="cmdCropCancel_Click" CssClass="divJustHidden" ClientIDMode="Static" />
			</div>
        </asp:Panel>
        
        <asp:Panel ID="panCrop" runat="server" CssClass="divCropImage" Visible="false">
            <div class="divUploadResizeCropArea divUploadResizeOutline"></div>
            
        	<div class="divUploadImageArea">
                <div id="divNewImageCrop">
                    <asp:Image ID="imgCrop" runat="server" />
                </div>
            </div>
            
            <asp:HiddenField ID="X" runat="server" />
            <asp:HiddenField ID="Y" runat="server" />
            <asp:HiddenField ID="W" runat="server" />
            <asp:HiddenField ID="H" runat="server" />
            
            <div class="divError">
                <asp:Label runat="server" ID="lblUploadCropError" Visible="false"></asp:Label>
            </div>
            
            <a href="javascript:void(0);" onclick="javascript:toggleLayer('divStep23Progress','divGrayCropImageBG','');window.setTimeout(function() {__doPostBack('<%= cmdCrop.UniqueID %>','');}, 500);" class="green-button">Crop</a>
           	<a href="javascript:void(0);" onclick="javascript:__doPostBack('<%= cmdCropCancel.UniqueID %>','');" class="green-button">Cancel</a>
            <%--
             Text="Crop" OnClientClick="toggleLayer('divStep23Progress','divGrayCropImageBG','');" CssClass="green-button"
             Text="Cancel" CssClass="green-button"
            --%>
            <asp:Button runat="server" ID="cmdCrop" OnClick="cmdCrop_Click" CssClass="divJustHidden" ClientIDMode="Static" /> 
            <asp:Button runat="server" ID="cmdCropCancel" OnClick="cmdCropCancel_Click" CssClass="divJustHidden" ClientIDMode="Static" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>

<asp:UpdatePanel ID="ajaxImage" runat="server">
    <ContentTemplate>
    	<asp:Panel runat="server" ID="panImageMain">
            <div id='divImageMainContainer' class='customContainer divFHImageMapDisplayContainer'>
                <div class="customContainer divFHSectionDescriptionContainer">
                    <div class="customLeft divFHSectionDescriptionLeft">
                    	<div id='divFHTransitionEffectImage'>
	                    	<label>Please select your prefered photo transition effect</label>
						</div>
                        <asp:DropDownList runat="server" ID="ddlTransitionModes" CssClass="selectTransitionMode" AutoPostBack="true" OnSelectedIndexChanged="ddlTransitionModes_SelectedIndexChanged">
                            <asp:ListItem Text="Fading" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Sweeping" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="customRight divFHSectionDescriptionRight divNumberMoreImage" id="divNumberMoreImage">
                        <asp:Literal runat="server" ID="litMaxImage"></asp:Literal>
                    </div>
                    <div class="customFooter divFHSectionDescriptionFooter"></div>
                </div>
                <div class="divBasicMessage" id="divImageMessage"></div>
                <div id='divFHPrimaryDisplayImage'>
                    <label>This is your primary image</label>
                </div>
                <div id='divDisplayCropImage'>
	                <asp:Literal runat="server" ID="litImage"></asp:Literal>
				</div>                
                <div id="divGaryImageFooter">
                	<div class="customContainer divFHSectionDescriptionContainer">
                        <div class="customLeft divFHSectionDescriptionLeft">
                            <asp:Literal runat="server" ID="litImageRightHeader" Text="<label class='lblSmallGaryText'>(drag and drop to change order)</label>"></asp:Literal>
                        </div>
                        <div class="customRight divFHSectionDescriptionRight">
		                    <label>Click on X to remove an image</label>
                        </div>
                        <div class="customFooter divFHSectionDescriptionFooter"></div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfCurrentImages" runat="server" />
		</asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
	jQuery(function($) {
		<asp:Literal runat="server" ID="litCrop"></asp:Literal>
				
		$("#divDisplayCropImage").disableSelection();
		$("#divDisplayCropImage").sortable({
			update: function(event, ui) {
				//resets the current order into hfCurrentImages
				$('#<%= hfCurrentImages.ClientID %>').val(classGetImageSRC(getDocID('divImageMainContainer')));
				<asp:Literal runat="server" ID="litSortCropImage"></asp:Literal>
			}
		});
		
		$('#<%= imgResize.ClientID %>').resizable({ 
			maxWidth: 996,
			maxHeight: 996,
			aspectRatio: true,
			/*minWidth: <%= WResize.Value %>,
			minHeight: <%= HResize.Value %>,*/
			resize: function(event, ui) {
				var sizeCurrent = ui.size;
				
				$('#<%= WResize.ClientID %>').val(sizeCurrent.width);
				$('#<%= HResize.ClientID %>').val(sizeCurrent.height);
		
				//beacuse both IE and firefox move the buttons already the resize button
				//do not need to be moved
				if (!navigator.userAgent.match(/Firefox/i) && !navigator.userAgent.match(/msie/i))
				{
					//checks if the image is bigger then the orginal height 
					if(sizeCurrent.height > $('#<%= panUploadResizeOrginalArea.ClientID %>').height())	
						//moves the buttons down as the image gets bigger
						$('#divResizeButton').css("marginTop", (sizeCurrent.height - $('#<%= panUploadResizeOrginalArea.ClientID %>').height()) + 15);
				}//end of if
			}
		});
		
		$('#divDisplayCropImage').draggable({
    		appendTo: 'body',
    		start: function(event, ui) {
        		isDraggingMedia = true;
    		},
    		stop: function(event, ui) {
        		isDraggingMedia = false;
    		}
		});
	});
	
	<asp:Literal runat="server" ID="litCropImageClick"></asp:Literal>
		
	function storeCoords(c) {
	  $('#<%= X.ClientID %>').val(c.x);
	  $('#<%= Y.ClientID %>').val(c.y);
	  $('#<%= W.ClientID %>').val(c.w);
	  $('#<%= H.ClientID %>').val(c.h);
	};
</script>