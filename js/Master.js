/*

	This code is to allow Javascript to communcted to the Server in order to update it
	
*/

//
// Define a list of Microsoft XML HTTP ProgIDs.
//
var XMLHTTPREQUEST_MS_PROGIDS = new Array(
  "Msxml2.XMLHTTP.7.0",
  "Msxml2.XMLHTTP.6.0",
  "Msxml2.XMLHTTP.5.0",
  "Msxml2.XMLHTTP.4.0",
  "MSXML2.XMLHTTP.3.0",
  "MSXML2.XMLHTTP",
  "Microsoft.XMLHTTP"
);

//
// Define ready state constants.
//
var XMLHTTPREQUEST_READY_STATE_UNINITIALIZED = 0;
var XMLHTTPREQUEST_READY_STATE_LOADING       = 1;
var XMLHTTPREQUEST_READY_STATE_LOADED        = 2;
var XMLHTTPREQUEST_READY_STATE_INTERACTIVE   = 3;
var XMLHTTPREQUEST_READY_STATE_COMPLETED     = 4;

var mapGoogle;//holds the google map object
var arrAttachMarkerGoogle = null;//holds all the markers that are attach to mapGoogle

//
// Returns XMLHttpRequest object. 
//
function getXMLHttpRequest()
{
  var httpRequest = null;

  // Create the appropriate HttpRequest object for the browser.
  if (window.XMLHttpRequest !== null)
    httpRequest = new window.XMLHttpRequest();
  else if (window.ActiveXObject !== null)
  {
    // Must be IE, find the right ActiveXObject.
    var success = false;
    for (var i = 0;i < XMLHTTPREQUEST_MS_PROGIDS.length && !success;i++)
    {
      try
      {
        httpRequest = new ActiveXObject(XMLHTTPREQUEST_MS_PROGIDS[i]);
        success = true;
      }
      catch (ex)
      {}
    }
  }

  // Display an error if we couldn't create one.
  if (httpRequest === null)
    alert("Error in HttpRequest():\n\nCannot create an XMLHttpRequest object.");

  // Return it.
  return httpRequest;
}

//Adds text to any part of the body of a HTML
function addNode(tagParent,strText,boolAddToBack, boolRemoveNode)
{
	var strNode = document.createTextNode(strText);//holds the test which will be added

	//gets the properties of the node
	tagParent = getDocID(tagParent);
	
	//checks if the user whats to replace the node in order to start with a clean slate
	//it also checks if there is a chode node to replace
	if (boolRemoveNode === true && tagParent.childNodes.length > 0)
		//replaces the current node with what the user wants
		tagParent.replaceChild(strNode,tagParent.childNodes[0]);
	else
	{
		//checks if the user whats to added to the back of the id or the front
		if(boolAddToBack === true)
			tagParent.appendChild(strNode);
		else
			//This is a built-in function of Javascript will add text to the beginning of the child
			insertBefore(strNode,tagParent.firstChild);
	}//end of if else
	
	//returns the divParent in order for the user to use it for more uses
	return tagParent;
}//end of addNode()

//changes the image of tagImage to what is in strImageSrc
function changeImage(tagImage,strImageSrc)
{
    //gets the properties of tagImage
    tagImage = getDocID(tagImage);
    
    //checks if there is a properties
    if(tagImage !== null)
        tagImage.src = strImageSrc;
}//end of changeImage()

//changes the FH offering change for the hidden
function changeFHOffering(tagOtherOfferingsValue,tagOtherOfferings)
{
	var arrOtherOfferings = tagOtherOfferingsValue.value.split("#2@");//holds the other offering values
	
	//resets hfOtherOfferingsValue
	tagOtherOfferingsValue.value = "";
	
	//goes around adding back all of the values with the exection of the new text verion
	for(var intIndex = 0;intIndex < arrOtherOfferings.length;intIndex++)
	{
		//checks if this is the txtbox
		if(tagOtherOfferings.id === "dnn_ctr582_FuneralHomeEditor_txtOtherOfferings" + intIndex)
			//add the changes the user has done
			tagOtherOfferingsValue.value += tagOtherOfferings.value + "#2@";
		else
			//adds back the value as it has not change
			tagOtherOfferingsValue.value += arrOtherOfferings[intIndex] + "#2@";
	}//end of for loop
}//end of changeFHOffering()

//changes the the Funeral Home name and id of what the user selected in order for the user to use this Funeral Home
function changeFHSelection(tagFHListing, tagViewState, tagClearAll, tagCurrentlySelected, strFHName, strFHID, strAddressFormat)
{
	//gets the properties of tagFHListing, tagClearAll, tagCurrentlySelected
	tagFHListing = getDocID(tagFHListing);
	tagClearAll = getDocID(tagClearAll);
	tagCurrentlySelected = getDocID(tagCurrentlySelected);
	
	//checks if there is a field tagFHListing
	//if so then add the name and id to it
	if(tagFHListing !== null)
	{
		var optNewItem = document.createElement("Option");//holds the new option for the list box
		
		//gets the proerties of a hidden field that will hold the items when the page goes does a postback
		tagViewState = getDocID(tagViewState);
		
		//sets the properties to the new item
		optNewItem.text = strFHName + ' - ' + strAddressFormat;
		optNewItem.value = strFHID;
		
		//adds a new item to the listbox
		tagFHListing.add(optNewItem);
		
		//sets the items into the hidden field
		tagViewState.value += "@*" + strFHName + "@*" + strFHID + "@*" + strAddressFormat;
	}//end of if
			
	//checks if all of the FH have been find as well as the field tagClearAll and tagCurrentlySelected
	//in order to remove the clear section
	if(tagFHListing !== null && tagClearAll !== null && tagCurrentlySelected !== null)
	{
		//displays the clear section and display what is being selected
		tagClearAll.style.display = "block";
		tagCurrentlySelected.style.display = "block";
		getDocID('divSearchResults').style.display = '';
	}//end of if
}//end of changeFHSelection()

//checks the current language of the site base on what is being selected
function checkLanguage(tagContainer,strClassName,strTAGName)
{
	//gets the search bar properties
	var arrTAG = getDocID(tagContainer).getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer
	var strLang = "English";//holds the language

	//goes around the for each tag that getElementsByTagName found in tagContainter
	for(var intIndex = arrTAG.length - 1; intIndex > -1 ; intIndex--) 
	{
		//checks if the class name is the same as strClassName
		if(arrTAG[intIndex].className === strClassName && arrTAG[intIndex].title === "Français")
			//gets the language
			strLang = arrTAG[intIndex].title;
	}//end of for loop
	
	return strLang;
}//end of checkLanguage()

//removes from view all tags in tagContainer with the expection of tagActive
//It assumes the tagGrayOut, tagActive and tagContiner already have the properties
function classActiveToggleLayer(tagContainer,tagActive,tagGrayOut,tagMedia,strClassName,strTAGName)
{
	var arrTAG = tagContainer.getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer
		
	//goes around the for each tag that getElementsByTagName found in tagContainter
	for(var intIndex = arrTAG.length - 1; intIndex > -1; intIndex--) 
	{
		//checks if there is a tagActive to use if not then remove all items with the same class name
		if(tagActive !== null)
		{
			//checks if the class name is the same as strClassName and it is not active if it is active then change the dispaly to block and tagGrayOut to be active for that active tag
			if(arrTAG[intIndex].className === strClassName && arrTAG[intIndex].id !== tagActive.id)
				arrTAG[intIndex].style.display = "";
			else if(arrTAG[intIndex].id === tagActive.id)	
			{						
				arrTAG[intIndex].style.display = arrTAG[intIndex].style.display? "":"block";
				
				//adds the tagGrayOut
				tagGrayOut.style.display = "block";
				
				//add event to deactived the Menu if the use clicks on the tagGrayOut
								
				//for IE
				if (navigator.userAgent.indexOf('MSIE') !== -1)
				{
					tagGrayOut.attachEvent('onclick',function () {
						classActiveToggleLayer(tagContainer,null,tagGrayOut,tagMedia,strClassName,strTAGName);
					});
				}//end of if
				//for the other browsers
				else
				{
					tagGrayOut.addEventListener('click',function () {
						classActiveToggleLayer(tagContainer,null,tagGrayOut,tagMedia,strClassName,strTAGName);
					},false);
				}//end of else
			}//end of else if
		}//end of if
		//disables all items with the same class name as this is the event by the tagGrayOut 
		else if(arrTAG[intIndex].className === strClassName)
		{
			//removes the tagGrayOut
			tagGrayOut.style.display = "";
			
			//changes the background of the active to be actived or deactive
			classToggleLayerChangeClass(getDocID('mainnav'),arrTAG[intIndex],'liActiveMenu', 'strCurrentClassName','dropdown','li');
			
			arrTAG[intIndex].style.display = "";
		}//end of else if
	}//end of for loop
}//end of classActiveToggleLayer()

//removes from view all tags in tagContainer
//It assumes the tagActive and tagContiner already have the properties
function classGetImageSRC(tagContainer)
{
	var strNewImageOrder = "";//holds the image order 

	//checks if there is any tagContainer get the tags from
	if(tagContainer !== null)
	{
		var arrTAG = tagContainer.getElementsByTagName("img");//holds all strTAGName in tagContainer
		
		//goes around the for img in tagContainer and find the current order of the images
		//and returns them to the user
		for(var intIndex = 0; intIndex < arrTAG.length; intIndex++) 
		{
			//sets the new order of the images in tagContainer
			strNewImageOrder += "@*" + arrTAG[intIndex].src.substring((arrTAG[intIndex].src.lastIndexOf("/") + 1));
		}//end of for loop
	}//end of if
	
	return strNewImageOrder;
}//end of classGetImageSRC()

//removes from view all tags in tagContainer with the expection of tagActive
//It assumes the tagActive and tagContiner already have the properties
function classToggleLayer(tagContainer,tagActive,strClassName,strTAGName)
{
	var arrTAG = tagContainer.getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer

	//goes around the for each tag that getElementsByTagName found in tagContainter
	for(var intIndex = arrTAG.length - 1; intIndex > -1; intIndex--) 
	{
		//checks if the class name is the same as strClassName and it is not active if it is active then change the dispaly to block
		if(arrTAG[intIndex].className === strClassName && arrTAG[intIndex].id !== tagActive.id)
			arrTAG[intIndex].style.display = arrTAG[intIndex].style.display? "":"";
		else if(arrTAG[intIndex].id === tagActive.id && tagActive.style.display === "" )
			arrTAG[intIndex].style.display = arrTAG[intIndex].style.display? "":"block";
	}//end of for loop
}//end of classToggleLayer()

//removes from view all tags in tagContainer with the expection of tagActive
//It assumes the tagActive and tagContiner already have the properties
function classToggleLayerChangeClass(tagContainer,tagActive,strActiveClassName,strClassName,strTAGName)
{
	var arrTAG = tagContainer.getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer
	
	//goes around the for each tag that getElementsByTagName found in tagContainter
	for(var intIndex = arrTAG.length - 1; intIndex > -1 ; intIndex--) 
	{
		//checks if the class name is the same as strClassName and it is not active if it is active then change the dispaly to block
		if(arrTAG[intIndex].id !== tagActive.id)
			arrTAG[intIndex].className = strClassName;
		else if(arrTAG[intIndex].id === tagActive.id)
			arrTAG[intIndex].className = strActiveClassName;
	}//end of for loop
}//end of classToggleLayerChangeClass()

//removes from view all tags in tagContainer with the expection of tagActive
//It assumes the tagActive and tagContiner already have the properties
function classToggleLayerChangeActiveClass(tagContainer, strActiveClassName, strClassName, strTAGName)
{
	//checks if there is any tagContainer get the tags from
	if(tagContainer !== null)
	{
		var arrTAG = tagContainer.getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer
		
		//goes around the for each tag that getElementsByTagName found in tagContainter
		for(var intIndex = arrTAG.length - 1; intIndex > -1 ; intIndex--) 
		{
			//checks if the class name is the same as the one we are searching for
			if(arrTAG[intIndex].className.indexOf(strClassName) === 0)
				//resets the class name to the current set
				arrTAG[intIndex].className = strActiveClassName;
		}//end of for loop
	}//end of if
}//end of classToggleLayerChangeActiveClass()

//counts from view all tags in tagContainer
//It assumes the tagContiner already have the properties
function classToggleLayerCounting(tagContainer,strClassName,strTAGName)
{
	var arrTAG = tagContainer.getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer
	var intTag = 0;//holds the number of tags that is using the same class name in the tagContainer
	
	//goes around the for each tag that getElementsByTagName found in tagContainter
	for(var intIndex = arrTAG.length - 1; intIndex > -1; intIndex--) 
	{
		//checks if the class name is the same as strClassName and if so then count it to the number of tags with the same class name
		if(arrTAG[intIndex].className === strClassName)
			intTag++;
	}//end of for loop
	
	return intTag;
}//end of classToggleLayerCounting()

//clear all selected Funeral Home in Sign Up
function clearAllFHSignUp(tagMessage, tagSearch, tagChooseFuneralHomeCurrentlySelected, tagChooseFuneralHomeBottom, tagChooseFHItem, tagChooseFH, boolDoClearAll, strPostBackLoc)
{
	//checks if the user wants to do a clear all or just some selected items
	if(boolDoClearAll === true)
	{
		//clears all parts of the choose a funeral home on sign up
		tagSearch.value = '';
		tagChooseFuneralHomeCurrentlySelected.style.display = '';
		tagChooseFuneralHomeBottom.style.display = '';
		tagChooseFHItem.value = '';
		tagChooseFH.innerHTML = '';
		
		//do a post back to reset the items
		__doPostBack("dnn$" + strPostBackLoc + "$ddlFuneralCountry","");
	}//end of if
	else
	{
		var arrSelectOption = getAllSelectOption(tagChooseFH);//holds all of the values that have been selected
		
		//checks if there is any items selected if not then tell the user so
		if(arrSelectOption.length === 0)
		{
			//displays the error message
			//displayMessage(tagMessage,'Select at least one item to be removed',true, true);
		}//end of if
		else
		{						
			//goes around finding the current seleted value from arrSelectOption and removes them from arrChooseFHItems
			for (var intIndex = 0;intIndex < arrSelectOption.length; intIndex = intIndex + 2)
			{
				var arrAddress = arrSelectOption[intIndex].split(' - ');//holds the address as it is used in the hidden field this is so that it can be remove from the hidden field
				var strFHName = arrAddress[0];//holds the name of the FH
				var strFHAddress = arrAddress[1];//holds the Address of the FH
				
				//goes around if arrAddress is more then 2 as it mean there are ' - ' in the name that need to be put
				//back into the name
				for (var intAddressIndex = 1;intAddressIndex < arrAddress.length && arrAddress.length > 2; intAddressIndex++)
				{
					//checks if this is the last item
					if(intAddressIndex === (arrAddress.length - 1))
						//adds the FH address which is the last item
						strFHAddress = arrAddress[(arrAddress.length - 1)];
					else
						//adds back the completed name if there is mor ehten 2 items in arrAddress
						strFHName += ' - ' + arrAddress[intAddressIndex];
				}//end of for loop
				
				//console.log("Item being remove: @*" + strFHName + "@*" + arrSelectOption[intIndex + 1] + "@*" + strFHAddress.trim());
				
				//finds the item in the arrChooseFHItems and replaces it with nothing
				tagChooseFHItem.value = tagChooseFHItem.value.replace("@*" + strFHName + "@*" + arrSelectOption[intIndex + 1] + "@*" + strFHAddress,"");
			}//end of for loop
								
			//checks if there is any items left in the choose a funeral home listing
			if(tagChooseFHItem.value === '')
				//clears all items as there is no more items to display
				clearAllFHSignUp(tagMessage, tagSearch, tagChooseFuneralHomeCurrentlySelected, tagChooseFuneralHomeBottom, tagChooseFHItem, tagChooseFH, true);
			else
				//do a post back to reset the items
				__doPostBack("dnn$" + strPostBackLoc + "$ddlFuneralCountry","");
		}//end of else
	}//end of else
}//end of clearAllFHSignUp()

//counts from view all tags in tagContainer
//It assumes the tagContiner and tagActive already have the properties
function clearAllOtherRadioChecks(tagContainer, tagActive, strTAGName)
{
	var arrTAG = tagContainer.getElementsByTagName(strTAGName);//holds all strTAGName in tagContainer
	
	//goes around the for each tag that getElementsByTagName found in tagContainter
	for(var intIndex = arrTAG.length - 1; intIndex > -1; intIndex--) 
	{
		//checks if this is not Active if so then make sure to turn off the check for this radiobutton
		if(arrTAG[intIndex].id !== tagActive.id)
			arrTAG[intIndex].checked = "";
	}//end of for loop
}//end of clearAllOtherRadioChecks()

//clear all selected User in FHSearch
function clearAllUserSignUp(tagMessage, tagSearch, tagChooseFuneralHomeCurrentlySelected, tagChooseFuneralHomeBottom, tagChooseFHItem, tagChooseFH, boolDoClearAll, strPostBackLoc)
{
	//checks if the user wants to do a clear all or just some selected items
	if(boolDoClearAll === true)
	{
		//clears all parts of the choose a funeral home on sign up
		tagSearch.value = '';
		tagChooseFuneralHomeCurrentlySelected.style.display = '';
		tagChooseFuneralHomeBottom.style.display = '';
		tagChooseFHItem.value = '';
		tagChooseFH.innerHTML = '';
		
		//do a post back to reset the items
		__doPostBack("dnn$" + strPostBackLoc + "$ddlFuneralCountry","");
	}//end of if
	else
	{
		var arrSelectOption = getAllSelectOption(tagChooseFH);//holds all of the values that have been selected
		
		//checks if there is any items selected if not then tell the user so
		if(arrSelectOption.length === 0)
		{
			//displays the error message
			//displayMessage(tagMessage,'Select at least one item to be removed',true, true);
		}//end of if
		else
		{						
			//goes around finding the current seleted value from arrSelectOption and removes them from arrChooseFHItems
			for (var intIndex = 0;intIndex < arrSelectOption.length; intIndex = intIndex + 2)
			{
				var arrEmail = arrSelectOption[intIndex].split(' - ');//holds the email as it is used in the hidden field this is so that it can be remove from the hidden field
								
				//finds the item in the arrChooseFHItems and replaces it with nothing
				tagChooseFHItem.value = tagChooseFHItem.value.replace("@*" + arrEmail[0] + "@*" + arrSelectOption[intIndex + 1] + "@*" + arrEmail[1],"");
			}//end of for loop

			//checks if there is any items left in the choose a funeral home listing
			if(tagChooseFHItem.value === '')
				//clears all items as there is no more items to display
				clearAllUserSignUp(tagMessage, tagSearch, tagChooseFuneralHomeCurrentlySelected, tagChooseFuneralHomeBottom, tagChooseFHItem, tagChooseFH, true);
			else
				//do a post back to reset the items
				__doPostBack("dnn$" + strPostBackLoc + "$ddlFuneralCountry","");
		}//end of else
	}//end of else
}//end of clearAllUserSignUp()

//convertds a date into UTC
function convertDateToUTC(dateSelected) 
{ 
	return new Date(dateSelected.getUTCFullYear(), dateSelected.getUTCMonth(), dateSelected.getUTCDate(), dateSelected.getUTCHours(), dateSelected.getUTCMinutes(), dateSelected.getUTCSeconds()); 
}//end of convertDateToUTC()

//decodes str to be a normal string in order to read it
function decodeURL(strDecode)
{
     return unescape(strDecode.replace(/\+/g, " "));
}//end of decodeURL()

//does the display the a message in a on the page weather then an alert
function displayMessage(tagMessage,strMessText,boolAddToBack, boolRemoveNode)
{
	//gets the message properties and sets the text furthermore it does the display
	tagMessage = addNode(tagMessage,strMessText,boolAddToBack, boolRemoveNode);
	tagMessage.style.display = "block";	
	
	return tagMessage;
}//end of displayMessage()

//this is for the duel layers that sometimes is need
function duelToggleLayer(whichLayer,layer1,layer2)
{
	var activeLayer = "";//holds the active Layer	
	var style2 = "";//holds the style of layer1
	var style3 = "";//holds the style of layer2

	// this is the way the standards work
	if (whichLayer !== ''){activeLayer = getDocID(whichLayer);}
	if (layer1 !== ''){style2 = getDocID(layer1);}
	if (layer2 !== ''){style3 = getDocID(layer2);}

	//Checks if there is an active layer
	if (activeLayer !== "")
	{
		//checks if the activeLayer is already active and if so then skips code
		//since the layer cannot be turn off and leave a hole in the review layer
		if (activeLayer.style.display === "") {
			//removes the block from the display in order to make the layer to disapper	
			if (style2 !== ''){style2.style.display = style2.style.display? "":"";}

			//checks if there is a style3
			if (style3 !== ''){style3.style.display = style3.style.display? "":"";}
	
			//displays the new active Layer and updates its id
			activeLayer.style.display = activeLayer.style.display? "":"block";
		}//end of if
	}//end of if
}//end of duelToggleLayer()

//encodes str to a URL so it can be sent over the URL address
function encodeURL(strEncode)
{
	var strResult = "";
	
	for (intIndex = 0; intIndex < strEncode.length; intIndex++) {
		if (strEncode.charAt(intIndex) === " ") strResult += "+";
		else strResult += strEncode.charAt(intIndex);
	}
	
	return escape(strResult);
}//end of encodeURL()

//gets the document properties in order to use them as there are many types of browers with different versions
function getDocID(tagLayer)
{
	var tagProp = "";//holds the proerties of tagLayer

	//gets the whichLayer Properties depending of the differnt bowers the user is using
	if (document.getElementById)//this is the way the standards work
		tagProp = document.getElementById(tagLayer);
	else if (document.all)//this is the way old msie versions work
		tagProp = document.all[tagLayer];
	else if (document.layers)//this is the way nn4 works
		tagProp = document.layers[tagLayer];
		
	return tagProp;
}//end of getDocID()

//gets the locaton of the strAddress
function getLocationGeo(strAddress,mapGoogle,boolMarkDraggable,tagMapLatitude,tagMapLongitude)
{
	geocoder = new google.maps.Geocoder();//holds the geocoder service object
	
	//finds the location of the address and then displays it on the map
	geocoder.geocode({'address': strAddress}, function(results, status) 
	{
		//checks if the locaiton of the can be found on a map
		if (status === google.maps.GeocoderStatus.OK) 
		{
			//centers the map to the location
			mapGoogle.setCenter(results[0].geometry.location);

			var marker = new google.maps.Marker({
				draggable: boolMarkDraggable,
				map: mapGoogle,
				position: results[0].geometry.location
			});//end of marker
			
			//sets the defualt for the latitude and the longitude
			tagMapLatitude.value = marker.getPosition().lat();
			tagMapLongitude.value = marker.getPosition().lng();

			//sets an event to change the latitude and longitude hidden fields			
			google.maps.event.addListener(marker, 'dragend', function (event) {
				tagMapLatitude.value = this.getPosition().lat();
				tagMapLongitude.value = this.getPosition().lng();
			});
		}//end of if
		else if(status === google.maps.GeocoderStatus.ZERO_RESULTS)
		{
			//centers the map to the location
			mapGoogle.setCenter(new google.maps.LatLng(43.64100156269233,-79.38599562435303));
			
			var markerZeroResutls = new google.maps.Marker({
				draggable: boolMarkDraggable,
				map: mapGoogle,
				position: new google.maps.LatLng(43.64100156269233,-79.38599562435303)
			});//end of marker
				
			//sets the defualt for the latitude and the longitude
			tagMapLatitude.value = markerZeroResutls.getPosition().lat();
			tagMapLongitude.value = markerZeroResutls.getPosition().lng();
				
			//sets an event to change the latitude and longitude hidden fields
			google.maps.event.addListener(markerZeroResutls, 'dragend', function (event) {
				tagMapLatitude.value = this.getPosition().lat();
				tagMapLongitude.value = this.getPosition().lng();
			});
		}//end of else if
		else
			alert('Geocode was not successful for the following reason: ' + status);
	});//end of geocoder.geocode
}//end of getLocationGeo()

//gets the locaton of the strAddress for the hidden Map
function getLocationHiddenGeo(strAddress, strTitle, intLocLat, intLocLng)
{
	var latLngDefault = new google.maps.LatLng(intLocLat, intLocLng);//holds the default of the location
	var geocoder = new google.maps.Geocoder();//holds the geocoder service object
	var infowindow = new google.maps.InfoWindow({
		content: '<div id="divHiddenGeoPopUp">' + 
			'<div id="divHiddenGeoPopUpTitle">' + 
				'<label>' + strTitle + '</label>' + 
			'</div>' + 
			'<div id="divHiddenGeoBody">' + 
				'<a href="http://maps.google.com/maps?daddr=' + encodeURL(strAddress) + '&saddr=" target="_blank">Get Directions</a>' + 
			'</div>' + 
		'</div>'
	});//holds the inforwindow that is above the marker
	
	//checks if the array has markers
	//if so then remove it from the markers from mapGoogle
	if(arrAttachMarkerGoogle && arrAttachMarkerGoogle.length !== 0)
	{
		//goes around removing all markers from the mapGoogle
		for(var intMarkerIndex = 0; intMarkerIndex < arrAttachMarkerGoogle.length; intMarkerIndex++) {
			//removes the marker by setting the map to null
			arrAttachMarkerGoogle[intMarkerIndex].setMap(null);
        }//end of for loop
    }//end of if
	
	//allowes to to add new markers to arrAttachMarkerGoogle
    arrAttachMarkerGoogle = [];
					
	//finds the location of the address and then displays it on the map
	geocoder.geocode({'address': strAddress}, function(results, status) 
	{
		//checks if the locaiton of the can be found on a map
		if (status === google.maps.GeocoderStatus.OK) 
		{						
			var marker = new google.maps.Marker({
				map: mapGoogle,
				position: results[0].geometry.location,
				title: strTitle
			});//end of marker
			
			//adds the marker to arrAttachMarkerGoogle
			arrAttachMarkerGoogle.push(marker);
			
			//opens the the pop up above this maker
			infowindow.open(mapGoogle,marker);
			
			//waits until google maps is loaded before centing the map as the information window
			//moves the map to the left
			window.setTimeout(function() {
				//sets the default location
				//centers the map to the location
				mapGoogle.setCenter(results[0].geometry.location);
			}, 100);
						
			//event to do a click
			google.maps.event.addListener(marker, 'click', function() {
				infowindow.open(mapGoogle, marker);
			});
		}//end of if
		else if(status === google.maps.GeocoderStatus.ZERO_RESULTS)
		{							
			var markerZeroResutls = new google.maps.Marker({
				map: mapGoogle,
				position: latLngDefault,
				title: strTitle
			});//end of marker
			
			//adds the marker to arrAttachMarkerGoogle
			arrAttachMarkerGoogle.push(markerZeroResutls);
			
			//opens the the pop up above this maker
			infowindow.open(mapGoogle,markerZeroResutls);
						
			//waits until google maps is loaded before centing the map as the information window
			//moves the map to the left
			window.setTimeout(function() {
				//sets the default location
				//centers the map to the location
				mapGoogle.setCenter(latLngDefault);
			}, 100);
			
			//event to do a click
			google.maps.event.addListener(markerZeroResutls, 'click', function() {
				infowindow.open(mapGoogle, markerZeroResutls);
			});
		}//end of else if
		else
			alert('Geocode was not successful for the following reason: ' + status);
	});//end of geocoder.geocode
}//end of getLocationHiddenGeo()

//gets the select option from tagSelect
function getSelectOption(tagSelect)
{
	var strSelectOption = "";//holds the select option the user has choosen
	
	//goes around finding the current seleted value from tagSelection
	for (var intIndex = 0;intIndex < tagSelect.options.length; intIndex++)
	{
		if (tagSelect.options[intIndex].selected === true)
			strSelectOption = tagSelect.options[intIndex].value;
	}//end of for loop
	
	return strSelectOption;
}//end of getSelectOption()

//gets the all select option from tagSelect when the user selected
function getAllSelectOption(tagSelect)
{
	var arrSelectOption = new Array([]);//holds the select option the user has choosen
	var intSelectIndex = 0;//holds the index for the select index
	
	//goes around finding the current seleted value from tagSelection
	for (var intIndex = 0;intIndex < tagSelect.options.length; intIndex++)
	{
		//checks if this item is selected
		if (tagSelect.options[intIndex].selected === true)
		{
			//adds it value to arrSelectOption
			arrSelectOption[intSelectIndex] = tagSelect.options[intIndex].text;
			arrSelectOption[intSelectIndex + 1] = tagSelect.options[intIndex].value;
			
			intSelectIndex = intSelectIndex + 2;
		}//end of if
	}//end of for loop
	
	return arrSelectOption;
}//end of getSelectOption()

//Read a page's GET URL variables and return them as an associative array.
function getUrlVars()
{
    var vars = [], hash;//holds the valuable value from the URL
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');//holds the section of of the value and valuable

	//goes around for each valuable in the URL
    for(var i = 0; i < hashes.length; i++)
    {
		//splites the value and valuable into half
        hash = hashes[i].split('=');
		
		//adds the valuable into first part ant the value into the secound pard
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }//end of for loop

    return vars;
}//end of getUrlVars()

//loads in the hidden map in order for it to be use when the user clicks on the map
function initializeHiddenMap(tagHiddenMap) 
{
	//truns on the hidden map for a bit in order for google maps to load into it
	getDocID('divHiddenHeaderMap').style.display = "block";
	
	var mapOptions = {
		center: new google.maps.LatLng(43.64100156269233,-79.38599562435303),
		mapTypeControl: true, 
		mapTypeControlOptions: { 
			style: google.maps.MapTypeControlStyle.DROPDOWN_MENU 
		}, 
		zoom: 15, 
		zoomControl: true, 
		zoomControlOptions: { 
			style: google.maps.ZoomControlStyle.SMALL 
		}, 
		mapTypeId: google.maps.MapTypeId.ROADMAP 
	};//holds the options of the map
	
	//sets a new insteance of the google map to the hidden map in order to add markers to it
	//in the search results page
	mapGoogle = new google.maps.Map(tagHiddenMap, mapOptions);
	
	//waits until google maps is loaded before turning off the hidden map
	window.setTimeout(function() {
		//truns off hidden map again as google maps has loaded
		getDocID('divHiddenHeaderMap').style.display = "";
		getDocID('divHiddenHeaderMap').style.visibility = "visible";
	}, 500);
}//end of initializeHiddenMap()

//does the click for the main search
function mainSearchClick(strMessageID, tagSearchText) {
	// checks if there is a text to display
	if(tagSearchText !== null) {
		// checks if there are eought char to do a search
		if(tagSearchText.value.length > 2)
			// sends the user to the search results page
			window.location = '/SearchResult.aspx?q=' + tagSearchText.value;
		else {
			displayMessage(strMessage, 'Please enter a minimum of 3 letters to start your search',true, true);
			return false;
		}// end of else
	}// end of if

	return true;
}//end of mainSearchClick

/**
 * does the search whent he user starts typing out the search text
 * @param  {String} strFileName          
 * @param  {String} strErrorMessageID    
 * @param  {Object} tagMainSearchResults 
 * @param  {Object} tagSearch            
 */
function mainSearchKeyUp(strFileName, strErrorMessageID, tagMainSearchResults, tagSearch) {
	// checks if this is not an up or down arraw as they are ued to to enter the search result dropdown
	if (event.keyCode !== 38 && event.keyCode !== 40) {
		// resets the search result
		tagMainSearchResults.style.display = "";

		// checks if there is the text is more then two charaters
		if(tagSearch.value.length > 2)
			// does the actully search
			sendSearchMain(strFileName, strErrorMessageID, tagMainSearchResults, tagSearch.value, '', 5, -1);

		// checks if there the enter key was nededr
		if (event.keyCode === 13 && tagSearch.value.length > 2)
			mainSearchClick(tagSearch);
		else if (event.keyCode === 13 && tagSearch.value.length <= 2)
			displayMessage(strErrorMessageID,'Please enter a minimum of 3 letters to start your search',true, true);
	}// end of if
}// end of mainSearchKeyUp()

//removes all new lines and replaces them with a <br/> html tag
function nl2br(strText)
{
	//checks if there is anything inside strText
	if (strText !== "")
	{
		var re_nlchar = "";//holds the different newlines that the OS uses
		
		strText = escape(strText);//in codes strText to be more like a URL to find the newlines
			
		//finds the either \r or \n or both since \r is for Linex and Apple and \n is for MS
		if(strText.indexOf('%0D%0A') > -1)
			re_nlchar = /%0D%0A/g ;
		else if(strText.indexOf('%0A') > -1)
			re_nlchar = /%0A/g ;
		else if(strText.indexOf('%0D') > -1)
			re_nlchar = /%0D/g ;
	
		//checks if there is any new lines in strText
		if (re_nlchar !== "")
			//changes the strText back to normal with all of the newlines changes to <br/> tag
			return unescape(strText.replace(re_nlchar,'<br />'));
	}//end of if
	
	return strText;
}//end of nl2br()

//set up the form to not be used while sending the message
function preSendEMail(tagMessage,strMessage,tagHiddenElement)
{
	//display to the user their message is beening sent and disables the textbox area
	displayMessage(tagMessage,strMessage,true,true);
	tagHiddenElement.style.display = 'none';
}//end of preSendEMail()

//sends the page to the search results page with the filters and which tab is active
function refineSearchResults(strSearchRefine, tagSearchResultPageTab)
{
	var arrTAG = tagSearchResultPageTab.getElementsByTagName('div');//holds all iv in tagSearchResultPageTab
	var strActiveTab = "";//holds which tab is active
	
	//checks if there is a ? in strSearchRefine
	if(strSearchRefine.indexOf('?') > 0)
		//add & to add to the URL valuable
		strActiveTab = "&";
	else
		//add ? to start the URL valuable
		strActiveTab = "?";

	//goes around the for each tag that getElementsByTagName found in tagSearchResultPageTab
	for(var intIndex = arrTAG.length - 1; intIndex > -1; intIndex--) 
	{
		//checks if this tag is the active one which is using the divSearchResultPageHeaderTabLeftSelection
		if(arrTAG[intIndex].className.indexOf("divSearchResultPageHeaderTabLeftSelection") > 0)
		{
			//sets the URL vauable with the activate tab
			strActiveTab += "t=" + intIndex;
			
			break;
		}//end of if
	}//end of for loop
	
	//goes to the search resutls with the filters and which tab is active
	window.location = strSearchRefine + strActiveTab;
}//end of refineSearchResults()

//gets the select option from tagSelect
function setSelectOption(tagSelect, strValue)
{
	var strSelectOption = "";//holds the select option the user has choosen
	
	//goes around finding the current seleted value from tagSelection
	for (var intIndex = 0;intIndex < tagSelect.options.length; intIndex++)
	{
		//checks if this is the value that the use wants to selected
		if (tagSelect.options[intIndex].value === strValue)
			strSelectOption = tagSelect.options[intIndex].selected = true;
	}//end of for loop
	
	return strSelectOption;
}//end of setSelectOption()

//sends a comend to remove a image for this Funeral Home
function sendDeleteFHImage(strFileName, tagMessage, tagMainImageContainer, tagImageContainer, tagCurrentImages, tagTimelineBody, tagAddSection, strImageName, intFHImageID, intFHID, intFHCurrentID, strFHDir, strPostBackLoc, intMax, tagCropImagesLeft, tagNumberMoreImage)
{
	var htmlJavaServerObject = getXMLHttpRequest();//holds the object of the server
		
	//Abort any currently active request.
	htmlJavaServerObject.abort();
		
	// Makes a request
	htmlJavaServerObject.open("Post", strFileName, true);
	htmlJavaServerObject.setRequestHeader("Content-Type","application/x-www-form-urlencoded");

	htmlJavaServerObject.onreadystatechange = function(){
		if(htmlJavaServerObject.readyState === 4 && htmlJavaServerObject.status === 200)
		{
			var arrActullyEndMassage = htmlJavaServerObject.responseText.split("</head>");//gets the acrtully end message because ASP.NET has alot of useless overhead
			
			//resets the error message
			displayMessage(tagMessage,'',true, true);
						
			//checks which if there is any errors from the server
			if(arrActullyEndMassage[1] === "\r\nDone\r\n")
			{
				//removes the image from view
				tagImageContainer.className = "customLeft divFHImageMapDisplayLeft divJustHidden";
				
				//removes the image from tagCurrentImages and in order for the image not to reload
				//when the page is does a postback
				tagCurrentImages.value = tagCurrentImages.value.replace("@*" + strImageName,"");
								
				var intNumberOfImages = classToggleLayerCounting(tagMainImageContainer,'customLeft divFHImageMapDisplayLeft','div');//holds the number of images that are in the timeline
				
				//checks if there is a tagCropImagesLeft as if the number of items is full
				//this then the countdown will be be displaying
				if(tagCropImagesLeft !== null)
					//sets the new number to the user
					tagCropImagesLeft.innerHTML = (intMax - intNumberOfImages);
				else
					//redisplay the countdown
					tagNumberMoreImage.innerHTML = "<label>Upload up to </label><label id='lblCropImagesLeft'>1</label><label> more images</label>";

				//checks if the number of images are either at the limit or none
				if(intNumberOfImages === intMax)
					//remove the Add Section as the limit of the number of images has been reach
					//this is done here to no have to reload the page
					tagAddSection.className = "divImageAddNew divJustHidden";					
				else if(intNumberOfImages === 0)
					//remove the time body as there is no images in there
					//this is done here to no have to reload the page
					tagTimelineBody.className = "divJustHidden";
				else
				{
					//turns on the timeline and add body
					tagTimelineBody.className = "";
					tagAddSection.className = "divImageAddNew";
				}//end of if
			}//end of if
			else
				displayMessage(tagMessage,arrActullyEndMassage[1],true, true);
		}//end of if
		else if(htmlJavaServerObject.readyState === 2 && htmlJavaServerObject.status === 500)
			displayMessage(tagMessage,'Unable to Connect to the Server.',true, true);
	};//end of function()

	htmlJavaServerObject.send("intFHImageID=" + intFHImageID + "&intFHID=" + intFHID + "&strFHIDDir=" + intFHCurrentID + "&strFHDir=" + strFHDir + "&strImageName=" + strImageName);
	
	return true;
}//end of sendDeleteFHImage()

//sends the search text for the search in the header to the server in order to get the display reseults
function sendSearchMain(strFileName, tagMessage, tagSearchResults, strSearchText, strSort, intMaxSearch, intSelectedTable)
{
	var htmlJavaServerObject = getXMLHttpRequest();//holds the object of the server
		
	//Abort any currently active request.
	htmlJavaServerObject.abort();
		
	// Makes a request
	htmlJavaServerObject.open("Post", strFileName, true);
	htmlJavaServerObject.setRequestHeader("Content-Type","application/x-www-form-urlencoded");

	htmlJavaServerObject.onreadystatechange = function(){
		if(htmlJavaServerObject.readyState === 4 && htmlJavaServerObject.status === 200)
		{
			var arrActullyEndMassage = htmlJavaServerObject.responseText.split("</head>");//gets the acrtully end message because ASP.NET has alot of useless overhead
			
			//resets the error message
			getDocID(tagMessage).style.display="";
						
			//displays the Search Results to the User
			tagSearchResults.innerHTML = arrActullyEndMassage[1];
			
			//checks which if there is any text to search and if so then 
			if(strSearchText !== "")
				//turn on the Search results
				tagSearchResults.style.display = "block";
			else
				//turns off the search results
				tagSearchResults.style.display = "";
		}//end of if
		else if(htmlJavaServerObject.readyState === 2 && htmlJavaServerObject.status === 500)
			displayMessage(tagMessage,'Unable to Connect to the Server.',true, true);
	};//end of function()

	htmlJavaServerObject.send("strSearchText=" + encodeURL(strSearchText) + "&strSort=" + encodeURL(strSort) + "&intMax=" + intMaxSearch + "&intSelectedTable=" + intSelectedTable);
	
	return true;
}//end of sendSearchMain()

//sends the search text for Sign Up Funeral Home to the server in order to get the display reseults
function sendSearchSignUpFuneralHome(strFileName, tagMessage, tagSearchResults, tagSearchText, intMaxSearch, strPostBackLoc, strChooseFH, strChooseFHItems, strChooseFuneralHomeBottom, strChooseFuneralHomeCurrentlySelected, strFuneralHomeStatus)
{
	var htmlJavaServerObject = getXMLHttpRequest();//holds the object of the server
		
	//Abort any currently active request.
	htmlJavaServerObject.abort();
	
	// Makes a request
	htmlJavaServerObject.open("Post", strFileName, true);
	htmlJavaServerObject.setRequestHeader("Content-Type","application/x-www-form-urlencoded");

	htmlJavaServerObject.onreadystatechange = function(){
		if(htmlJavaServerObject.readyState === 4 && htmlJavaServerObject.status === 200)
		{
			var arrActullyEndMassage = htmlJavaServerObject.responseText.split("</head>");//gets the acrtully end message because ASP.NET has alot of useless overhead
			
			//resets the error message
			displayMessage(tagMessage,'',true, true);
			
			//displays the Search Results to the User
			tagSearchResults.innerHTML = arrActullyEndMassage[1];
			
			//checks which if there is any text to search and if so then 
			if(tagSearchText.value !== "")
				//turn on the Search results
				tagSearchResults.style.display = "block";
			else
				//turns off the search results
				tagSearchResults.style.display = "";
		}//end of if
		else if(htmlJavaServerObject.readyState === 2 && htmlJavaServerObject.status === 500)
			displayMessage(tagMessage,'Unable to Connect to the Server.',true, true);
	};//end of function()

	htmlJavaServerObject.send("txtSearch=" + encodeURL(tagSearchText.value) + "&intMax=" + intMaxSearch + "&strPostBackLoc=" + strPostBackLoc + "&strChooseFH=" + strChooseFH + "&strChooseFHItems=" + strChooseFHItems +"&strChooseFuneralHomeBottom=" + strChooseFuneralHomeBottom + "&strChooseFuneralHomeCurrentlySelected=" + strChooseFuneralHomeCurrentlySelected + "&strFuneralHomeStatus=" + strFuneralHomeStatus);
	
	return true;
}//end of sendSearchSignUpFuneralHome()

//sends a comend to sort images
function sendSortFHImage(strFileName, tagMessage, tagImageOrder, intFHID, boolUseUser)
{
	var htmlJavaServerObject = getXMLHttpRequest();//holds the object of the server
		
	//Abort any currently active request.
	htmlJavaServerObject.abort();
		
	// Makes a request
	htmlJavaServerObject.open("Post", strFileName, true);
	htmlJavaServerObject.setRequestHeader("Content-Type","application/x-www-form-urlencoded");

	htmlJavaServerObject.onreadystatechange = function(){
		if(htmlJavaServerObject.readyState === 4 && htmlJavaServerObject.status === 200)
		{
			var arrActullyEndMassage = htmlJavaServerObject.responseText.split("</head>");//gets the acrtully end message because ASP.NET has alot of useless overhead
			
			//resets the error message
			displayMessage(tagMessage,'',true, true);
						
			//checks which if there is any errors from the server
			if(arrActullyEndMassage[1] === "\r\nDone\r\n")
			{
			}//end of if
			else
				displayMessage(tagMessage,arrActullyEndMassage[1],true, true);
		}//end of if
		else if(htmlJavaServerObject.readyState === 2 && htmlJavaServerObject.status === 500)
			displayMessage(tagMessage,'Unable to Connect to the Server.',true, true);
	};//end of function()

	htmlJavaServerObject.send("intFHID=" + intFHID + "&boolUseUser=" + boolUseUser + "&strImageOrder=" + encodeURL(tagImageOrder.value));
	
	return true;
}//end of sendSortFHImage()

//sends a does the uploading and the processing
function sendUploadImage(strFileName, tagProgress, tagMainImageContainer, tagImageContainer, tagCurrentImages, tagTimelineBody, tagAddSection, strImageName, intFHImageID, intFHID, intFHCurrentID, strFHDir, strPostBackLoc, intMax, tagCropImagesLeft)
{
	var htmlJavaServerObject = getXMLHttpRequest();//holds the object of the server
		
	//Abort any currently active request.
	htmlJavaServerObject.abort();
		
	// Makes a request
	htmlJavaServerObject.open("Post", strFileName, true);
	htmlJavaServerObject.setRequestHeader("Content-Type","application/x-www-form-urlencoded");

	htmlJavaServerObject.onreadystatechange = function(){
		if(htmlJavaServerObject.readyState === 4 && htmlJavaServerObject.status === 200)
		{
			var arrActullyEndMassage = htmlJavaServerObject.responseText.split("</head>");//gets the acrtully end message because ASP.NET has alot of useless overhead
			
			//resets the error message
			displayMessage(tagMessage,'',true, true);
						
			//checks which if there is any errors from the server
			if(arrActullyEndMassage[1] === "\r\nDone\r\n")
			{
				//removes the image from view
				tagImageContainer.className = "customLeft divFHImageMapDisplayLeft divJustHidden";
				
				//removes the image from tagCurrentImages and in order for the image not to reload
				//when the page is does a postback
				tagCurrentImages.value = tagCurrentImages.value.replace("@*" + strImageName,"");
								
				var intNumberOfImages = classToggleLayerCounting(tagMainImageContainer,'customLeft divFHImageMapDisplayLeft','div');//holds the number of images that are in the timeline
				
				//sets the new number to the user
				tagCropImagesLeft.innerHTML = (intMax - intNumberOfImages);

				//checks if the number of images are either at the limit or none
				if(intNumberOfImages === intMax)
					//remove the Add Section as the limit of the number of images has been reach
					//this is done here to no have to reload the page
					tagAddSection.className = "divImageAddNew divJustHidden";					
				else if(intNumberOfImages === 0)
					//remove the time body as there is no images in there
					//this is done here to no have to reload the page
					tagTimelineBody.className = "divJustHidden";
				else
				{
					//turns on the timeline and add body
					tagTimelineBody.className = "";
					tagAddSection.className = "divImageAddNew";
				}//end of if
			}//end of if
			else
				displayMessage(tagMessage,arrActullyEndMassage[1],true, true);
		}//end of if
		else if(htmlJavaServerObject.readyState === 2 && htmlJavaServerObject.status === 500)
			displayMessage(tagMessage,'Unable to Connect to the Server.',true, true);
	};//end of function()

	htmlJavaServerObject.send("intFHImageID=" + intFHImageID + "&intFHID=" + intFHID + "&strFHIDDir=" + intFHCurrentID + "&strFHDir=" + strFHDir + "&strImageName=" + strImageName);
	
	return true;
}//end of sendUploadImage()

//starts up the page
function startUp()
{
	var oldonload=window.onload;//holds any prevs onload function from the js file

	//gets the onload window event checks if there is a function that is already in there
	window.onload = function () {
		if (typeof (oldonload) === 'function')
			oldonload();
			
		//checks if the Google Maps funcation exisit
		//if so then run it
		if(typeof window.googleMapsInit === 'function')
			googleMapsInit();
			
		var tagBasicTab = getDocID("liBasicTab");//holds the first area of the FAQ
		var tagSearch = getDocID('divSearchResultPageBodyRight');//holds the search results body
		var tagHiddenMap = getDocID('divHiddenMap');//holds the hidden map
					
		//checks if this is the faq page if so then activated the first area
		if(tagBasicTab !== null)
			classToggleLayerChangeClass(getDocID('myTab'),tagBasicTab,'active','','li');
		
		//checks if there is a hidden map
		if(tagHiddenMap !== null)
		{
			//checks that it is only in search and obituray detail page
			if(getDocID('divSearchResultPageTab') !== null || getDocID('divObituarySummary') !== null)
				//loads in the hidden map for one of the lightbox
				initializeHiddenMap(tagHiddenMap);
		}//end fo if
	};//end of window.onload=function()
}//end of startUp()

//shoes and hides a <div> using display:block/none from the CSS
function toggleLayer(tagLayer,tagGrayOut,tagMedia)
{
	var tagStyle = '';//holds the style of tagLayer

	//gets the tagLayer and tagGrayOut Properties
	tagStyle = getDocID(tagLayer);
	tagGrayOut = getDocID(tagGrayOut);
	tagMedia = getDocID(tagMedia);
		
	if (tagStyle !== null)
	{tagStyle.style.display = tagStyle.style.display? "":"block";}
	
	if (tagGrayOut !== null)
	{
		tagGrayOut.style.display = tagGrayOut.style.display? "":"block";

		//for IE
		if (navigator.userAgent.indexOf('MSIE') !== -1)
		{
			tagGrayOut.attachEvent('onclick',function () {
				toggleLayer(tagStyle.id,tagGrayOut.id);
								
				//checks if there is any Media to stop also pleace remove when REUSING THIS FUNCTION 
				if (tagMedia !== null && document.getElementById("embed_url") !== null)
					tagMedia.removeChild(document.getElementById("embed_url"));
			});
		}//end of if
		//for the other browsers
		else
		{
			tagGrayOut.addEventListener('click',function () {
				toggleLayer(tagStyle.id,tagGrayOut.id);
				
			if (tagMedia !== null && document.getElementById("embed_url") !== null)
				tagMedia.removeChild(document.getElementById("embed_url"));

			},false);
		}//end of else
	}//end of if
}//end of toggleLayer()

//validates the any ASP.NET RegularExpressionValidator has been used
function validateExpressionCheck(tagExpValidator, tagFieldBeingCheck) 
{
	//gets the properties from the tags
	tagExpValidator = getDocID(tagExpValidator);
	tagFieldBeingCheck = getDocID(tagFieldBeingCheck);
	
	//checks if tagExpValidator is being used 
	if (tagExpValidator.style.display !== 'none')
		//add a class to hightlight the error in the field
		tagFieldBeingCheck.className = "txtDisplayError";
	else
		//resets the class
		tagFieldBeingCheck.className = "";
}//end of validateExpressionCheck()