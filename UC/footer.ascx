<%@ Control Language="C#" AutoEventWireup="True" CodeFile="footer.ascx.cs" ClassName="footer" Inherits="footer" %>

<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/language.ascx" %>

	</div>
</div>
<div class="OB-container-footer" align="center">
	<div class="OB-footer" align="left">
		<div class="OB-footerLeft">
	        <a href="/learn/overview.aspx">Learn</a><br />
	        <a href="/resources/faq.aspx">FAQ's</a><br />
	        <a href="/learn/aboutus/contactus.aspx">Contact</a>
	    </div>
        <div class="OB-footerMiddle">
            <div class="OB-footer-header">
            	Who We Are
			</div>
            <p>Our feature-rich Obituary Announcements, Memorials and Pre-Plan offerings, help people honour lives lived – respectfully and permanently in Canada’s National Registry. </p> 
        </div>    
    	<div class="OB-footerRight">
    		<div class="OB-footer-header">
            	<label>What's New?</label>
			</div>
            
            <div id="divWordpressRSSFeed"></div>
	    </div>    
	    <div class="OB-clear"></div>
		<div>
            <div class="OB-copyright" id="divFooterCopyright">
                Copyright 2013&nbsp;&nbsp;theObituaries.ca&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="/privacypolicy.aspx">Privacy Policy</a>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="/termsofuse.aspx">Terms of Use</a>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="/commercialtermsofuse.aspx">Commercial Terms for Consumers</a>&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div>
                <img src="/portals/_default/skins/obit/images/obit-social-footer.png" usemap="#Map"  />
                <map name="Map" id="Map"><area shape="rect" coords="0,0,16,30" href="http://www.facebook.com/theobituaries" target="_blank" />
    				<area shape="rect" coords="19,0,49,30" href="https://twitter.com/theobituariesca" target="_blank" />
    				<area shape="rect" coords="52,0,73,30" href="https://www.linkedin.com/company/the-obituaries-inc-" target="_blank" />
    			</map>
            </div>
            <div class="OB-footer-logo">
                <a href="/Default.aspx"><img src="/portals/_default/skins/obit/Images/ob-footer-logo.png" /></a>
            </div>
		</div>
	</div>
</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyD3Ev08AgAt6lLGIlIHq-q1hIwwER7cFvQ&sensor=false' type='text/javascript'></script>
<script src="https://platform.twitter.com/widgets.js"></script>
<script src="https://code.jquery.com/jquery-migrate-1.0.0.js"></script>
<script src="/Portals/_default/Skins/Obit/JS/jquery.html5-placeholder-shim.js"></script>
<script src="/Portals/_default/Skins/Obit/JS/Master.js" type="text/javascript" ></script>
<script src="/Portals/_default/Skins/Obit/JS/cufon-yui.js" type="text/javascript"></script>
<script src="/Portals/_default/Skins/Obit/JS/Trade_Gothic_20.font.js" type="text/javascript"></script>
<script src="/Portals/_default/Skins/Obit/JS/jquery.atooltip.js"></script>
<script src="/Portals/_default/Skins/Obit/js/bootstrap.min.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.core.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.widget.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.position.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.autocomplete.js"></script>    
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.progressbar.js"></script>    
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.datepicker.js"></script>
<script src="/Portals/_default/Skins/Obit/js/jquery.zrssfeed.min.js"></script>
<script src="/Portals/_default/Skins/Obit/js/plugins.js"></script>
<script src="/Portals/_default/Skins/Obit/js/jquery.printElement.min.js"></script>

<script type="text/javascript">
	$(document).ready(function () {
		$('#divWordpressRSSFeed').rssfeed('https://www.theobituaries.ca/blog/?feed=rss2', {
			limit: 2,
			header:false
		});
				
		//turns on the placeholder fix
		$.placeholder.shim();
	});
	
	var menuHidden = false;
	
	$("#divMainSearch").click(function() {
		if (menuHidden==false){
			$('#mainnav').animate({
				opacity: 0
			}, 200, function() {
				$(this).hide();
				$('#aMainSearch').animate({margin:'5px 0 0 643px'}, 200);
				$('#txtSearch').animate({width:'655px'}, 200);
				$('#divMainSearch').animate({width:'665px'}, 200);
				
				menuHidden = true;
			});
		}
	})

	/*
	
		Search Bar Events

	 */
	
	$(document).click(function() {
		if (menuHidden==true){			
			$('#txtSearch').animate({
				width: '333px'
			}, 300, function() {
				menuHidden = false;
				$('#mainnav').show();
				$('#mainnav').animate({opacity:1},300);
				
				getDocID('divMainSearchMessage').style.display = '';
			});
			$('#aMainSearch').animate({margin:'5px 0 0 319px'}, 300);
			$('#divMainSearch').animate({width:'345px'}, 300);
		}
	});

	$(document).keyup(function(event) {
		var $divSearchDropDownContainerSelect = $('div.divSearchDropDownContainer.divSearchDropDownContainerSelect');// holds the current div that is deleted

		// checks which keyboard button was press
		if(event.which === 13)
			// selected current 
			$divSearchDropDownContainerSelect.click();
	});
	
	$(document).keydown(function(event) {
		var $divSearchDropDownContainerSelect = $('div.divSearchDropDownContainer.divSearchDropDownContainerSelect');//holds the current div that is deleted
		var $arrCurrentBeingSelected = $('div.divSearchDropDownContainer');//holds the all items being that should be selected

		//checks which keyboard button was press
		switch (event.which) {
			case 38:
				//checks if the prev div is a sub header and not a item that can be selected
				if($divSearchDropDownContainerSelect.prev().hasClass("divSearchItemSubHeader") == false) {
					//removes the class from the current item that is selected
					$divSearchDropDownContainerSelect.removeClass('divSearchDropDownContainerSelect').prev().addClass('divSearchDropDownContainerSelect');
	
					//checks if this there is an item one up from this item
					if ($divSearchDropDownContainerSelect.prev().length == 0)
						//adds the class for the current item being selected
						$arrCurrentBeingSelected.eq(-1).addClass('divSearchDropDownContainerSelect');
				}//end of if
				//skips over the subheader and goes to the prev item
				else {
					//removes the class from the current item that is selected
					$divSearchDropDownContainerSelect.removeClass('divSearchDropDownContainerSelect').prev().prev().addClass('divSearchDropDownContainerSelect');
					
					//checks if this there is an item one down from this item
					if ($divSearchDropDownContainerSelect.prev().prev().length == 0)
						//adds the class for the current item being selected
						$arrCurrentBeingSelected.eq(-1).addClass('divSearchDropDownContainerSelect');
				}//end of if
			break;
			case 40:
				//checks if the next div is a sub header and not a item that can be selected
				if($divSearchDropDownContainerSelect.next().hasClass("divSearchItemSubHeader") == false) {
					//removes the class from the current item that is selected
					$divSearchDropDownContainerSelect.removeClass('divSearchDropDownContainerSelect').next().addClass('divSearchDropDownContainerSelect');
					
					//checks if this there is an item one down from this item
					if ($divSearchDropDownContainerSelect.next().length == 0)
						//adds the class for the current item being selected
						$arrCurrentBeingSelected.eq(0).addClass('divSearchDropDownContainerSelect');
				}//end of if
				//skips over the subheader and goes to the next item
				else {
					//removes the class from the current item that is selected
					$divSearchDropDownContainerSelect.removeClass('divSearchDropDownContainerSelect').next().next().addClass('divSearchDropDownContainerSelect');
					
					//checks if this there is an item one down from this item
					if ($divSearchDropDownContainerSelect.next().next().length == 0)
						//adds the class for the current item being selected
						$arrCurrentBeingSelected.eq(0).addClass('divSearchDropDownContainerSelect');
				}//end of if
			break;
		}//end of switch
	});
	
	//Learn Section
	
	// tooltip
	$(".tooltip-zt1").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Display and share multiple service dates, times and locations - quickly and easily.'
	});
	$(".tooltip-zt2").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Friends may offer private or public condolences. They can even share through social media app’s like Facebook. '
	});
	$(".tooltip-zt3").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Finding Funeral Homes or host service locations has never been easier with quick links to maps and directions.'
	}); 
   $(".tooltip-zt4").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Integrated access to allow guests to make a donation in the name of your loved one – quickly and securely – including confirmation and receipt. '
	});
	$(".tooltip-zt5").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Browse and purchase arrangements to be delivered to the Funeral Home, family or person of your choice – we have already looked after the address details for you.'
	});
	$(".tooltip-zt6").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Select a card, write your own personal message and we’ll take it from there – all from the convenience of your home.'
	});  
   $(".tooltip-zt7").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Have confidence your announcements will reach the people who need to know more quickly, through tasteful email notifications and social media sharing.'
	});
	$(".tooltip-zt8").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Yes, perpetual. As the ‘National Registry’ for hosting obituaries and memorials, our objective is to host, maintain and share the details indefinitely.  '
	});
	$(".tooltip-zt9").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'With just one easy step, you can subscribe to receive anniversary reminders of a passing, all notices from a specific Funeral Home, or updates to service dates, times and locations.'
	}); 
   $(".tooltip-zt10").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Utilize our Obituary or Pre-Planned Obituary services, and your announcement transitions to a Memorial where you may continue to add many more photos and stories – when the time is right for you.'
	});
	$(".tooltip-zt11").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'There are no annual fees or additional charges to utilize more features. And all of this is available at a fraction of the cost most newspapers charge for a single day notice.'
	});
	$(".tooltip-zt12").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Come back when the time is right or as often as you wish to make changes, additions, or edits. Write as much as you want – just imagine the stories you could share.'
	});                
   $(".tooltip-zt13").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Display current and most recent services making it easy for people to plan & attend.'
	});  
   $(".tooltip-zt14").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Help people locate your Funeral Home or other sites that services may be hosted at.'
	});
	$(".tooltip-zt15").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Proudly display all of your service offerings available for families and communities.'
	});
	$(".tooltip-zt16").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Contact names, email addresses, phone numbers or fax – whatever is important to help people reach you.'
	}); 
   $(".tooltip-zt17").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'If you have an existing site you still wish to link to – we can do that too.'
	});
	$(".tooltip-zt18").aToolTip({
		toolTipClass: 'mytheme',
		tipContent: 'Display details about your people, history, services or anything else that’s important – and share as much as you wish – it’s unlimited.'
	});/**/
  
	$('ul.undermenu-list li  a').click(function(){
		var submenus = $("ul.undermenu-list li");
		submenus.removeClass("active");
		$(this).parent().addClass("active");
		 
		
		var df = $(this).attr('href');
		updateSlider();
		$('.content-subtabs ul, .content-subtabs #sub-funeral, .content-subtabs #subsresources ').css('display', 'none');
		var sad= '.content-subtabs '+df;
		$(sad).css('display','block');
		return false;
	});
	
	//

    Cufon.replace('#gothic1',  { fontWeight: 'normal' }); // Requires a selector engine for IE 6-7, see above
	Cufon.replace('#gothic11', { fontWeight: 'normal' });
	Cufon.replace('#gothic12', { fontWeight: 'normal' });
	Cufon.replace('#gothic13', { fontWeight: 'normal' });
	Cufon.replace('#gothic14', { fontWeight: 'normal' });
	Cufon.replace('#gothic15', { fontWeight: 'normal' });
	Cufon.replace('#gothic16', { fontWeight: 'normal' });
    Cufon.replace('#gothic2',  { fontWeight: 'normal' });
	Cufon.replace('#gothic21', { fontWeight: 'normal' });
	Cufon.replace('#gothic22', { fontWeight: 'normal' });
	Cufon.replace('#gothic23', { fontWeight: 'normal' });
	Cufon.replace('#gothic24', { fontWeight: 'normal' });
	Cufon.replace('#gothic25', { fontWeight: 'normal' });
	Cufon.replace('#gothic3',  { fontWeight: 'normal' });
	Cufon.replace('#gothic31', { fontWeight: 'normal' });
	Cufon.replace('#gothic32', { fontWeight: 'normal' });
	Cufon.replace('#gothic33', { fontWeight: 'normal' });
	Cufon.replace('#gothic34', { fontWeight: 'normal' });
	Cufon.replace('#gothic44', { fontWeight: 'normal' });
	Cufon.replace('#gothic45', { fontWeight: 'normal' });	
	Cufon.replace('#gothic46', { fontWeight: 'normal' });
	Cufon.replace('#gothic47', { fontWeight: 'normal' });
	Cufon.replace('#gothic48', { fontWeight: 'normal' });
	Cufon.replace('#gothic49', { fontWeight: 'normal' });
	Cufon.replace('#gothic50', { fontWeight: 'normal' });
	Cufon.replace('#gothic100', { fontWeight: 'normal' });	
	Cufon.replace('#gothic101', { fontWeight: 'normal' });	
	Cufon.replace('#gothic102', { fontWeight: 'normal' });	
	Cufon.replace('#gothic103', { fontWeight: 'normal' });
	Cufon.replace('#gothic104', { fontWeight: 'normal' });
	Cufon.replace('#gothic105', { fontWeight: 'normal' });
	Cufon.replace('#gothic106', { fontWeight: 'normal' });
	Cufon.replace('#gothic107', { fontWeight: 'normal' });
	Cufon.replace('#gothic108', { fontWeight: 'normal' });
	Cufon.replace('#gothic109', { fontWeight: 'normal' });
	Cufon.replace('#gothic110', { fontWeight: 'normal' });
	Cufon.replace('#gothic111', { fontWeight: 'normal' });
	Cufon.replace('#gothic112', { fontWeight: 'normal' });
	Cufon.replace('#gothic113', { fontWeight: 'normal' });
	Cufon.replace('#gothic114', { fontWeight: 'normal' });	
	Cufon.replace('#gothic1110', { fontWeight: 'normal' });
	Cufon.replace('#gothic1111', { fontWeight: 'normal' });
	Cufon.replace('#gothic1112', { fontWeight: 'normal' });
	Cufon.replace('#gothic1113', { fontWeight: 'normal' });
	Cufon.replace('#gothic1114', { fontWeight: 'normal' });
	Cufon.replace('#gothic1115', { fontWeight: 'normal' });
	Cufon.replace('#gothic1116', { fontWeight: 'normal' });
	Cufon.replace('#gothic1117', { fontWeight: 'normal' });
	Cufon.replace('#gothic1118', { fontWeight: 'normal' });
	Cufon.replace('#gothic1119', { fontWeight: 'normal' });
	Cufon.replace('#gothic1120', { fontWeight: 'normal' });
	Cufon.replace('#gothic1121', { fontWeight: 'normal' });
	Cufon.replace('#gothic1122', { fontWeight: 'normal' });
	Cufon.replace('#gothic1123', { fontWeight: 'normal' });
	Cufon.replace('#gothic1124', { fontWeight: 'normal' });
	Cufon.replace('#gothic1125', { fontWeight: 'normal' });
	Cufon.replace('#gothic1126', { fontWeight: 'normal' });
	Cufon.replace('#gothic1127', { fontWeight: 'normal' });
	Cufon.replace('#gothic1128', { fontWeight: 'normal' });
	Cufon.replace('#gothic1129', { fontWeight: 'normal' });
	Cufon.replace('#gothic1010', { fontWeight: 'normal' });
	Cufon.replace('#gothic1011', { fontWeight: 'normal' });
	Cufon.replace('#gothic1012', { fontWeight: 'normal' });
	
	<!-- Gothic for for header area only -->
	Cufon.replace('#gothicHeader1', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader2', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader3', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader4', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader5', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader6', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader7', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader8', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader9', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader10', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader11', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader12', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader13', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader14', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader15', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader16', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader17', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader18', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader19', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader20', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader21', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader22', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader23', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader24', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader25', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader26', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader27', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader28', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader29', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader30', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader31', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader32', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader33', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader34', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader35', { fontWeight: 'normal' });
	Cufon.replace('#gothicHeader36', { fontWeight: 'normal' });
	
	<!-- Gothic for for UC only -->
	Cufon.replace('#gothicUC1', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC2', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC3', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC4', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC5', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC6', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC7', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC8', { fontWeight: 'normal' });
	Cufon.replace('#gothicUC9', { fontWeight: 'normal' });
	
	<!-- Gothic for for Search only -->
	Cufon.replace('#gothicSearch1', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch2', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch3', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch4', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch5', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch6', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch7', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch8', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch9', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch10', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch11', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch12', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch13', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch14', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch15', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch16', { fontWeight: 'normal' });
	Cufon.replace('#gothicSearch17', { fontWeight: 'normal' });
	
	<!-- Gothic for for SubMenu only -->
	Cufon.replace('#gothicSubMenu0', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu2', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu3', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu4', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu5', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu6', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu7', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu8', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu9', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu10', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu11', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu12', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu13', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu14', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu15', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu16', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu17', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu18', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu19', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu110', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu111', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu112', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu113', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu114', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu115', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu116', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu117', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu118', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu119', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1110', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1111', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1112', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1113', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1114', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1115', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1116', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1117', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1118', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1119', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1120', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1121', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1122', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1123', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1124', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1125', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1126', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1127', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1128', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1129', { fontWeight: 'normal' });
	Cufon.replace('#gothicSubMenu1130', { fontWeight: 'normal' });

	startUp();
</script>
