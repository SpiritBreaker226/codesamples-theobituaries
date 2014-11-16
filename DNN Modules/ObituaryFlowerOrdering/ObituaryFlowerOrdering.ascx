<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObituaryFlowerOrdering.ascx.cs" Inherits="ObituaryFlowerOrdering" %>

<%@ Register TagPrefix="uc1" TagName="FlowerHeader" Src="~/Portals/_default/Skins/Obit/UC/FlowerHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FlowerFooter" Src="~/Portals/_default/Skins/Obit/UC/FlowerFooter.ascx" %>

<SCRIPT LANGUAGE="JavaScript">
<!--begin
function popup(filename){
var centerWidth=(screen.width/2)-(380/2);
var centerHeight=(screen.height/2)-(250/2);
window.open(filename, "","height=430,width=400,top="+centerHeight+",left="+centerWidth+",location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,directories=no");
} 

</SCRIPT>


<uc1:FlowerHeader runat="server" ID="FlowerHeader" />

<asp:UpdatePanel ID="ajaxObituaryFlowerShoppingCart" runat="server">
    <ContentTemplate>
    	<div class="divError">
            <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
        </div>
    
        <asp:Panel runat="server" ID="panNoCart" Visible="false">
            <div id="divNoCart">
                <label id="gothic100" class="lblFlowerItem">No items in cart</label>
            </div>
            <asp:Button runat="server" ID="cmdBack" CssClass="green-button" Text="Continue Shopping" OnClick="cmdBack_Click"></asp:Button>
        </asp:Panel>
        <asp:Panel runat="server" ID="panBody">
        	<asp:Panel runat="server" ID="panShoppingCart">
                <div id="divPaymentBody">
                    <div class="customContainer divObiturayFlowersOrderingShoppingCartContainer" id="divFlowersOrderingText">
                        <div class="customLeft divObiturayFlowersOrderingShoppingCartLeft">
                            <label class="lblFlowerItem">Your Selection(s)</label>
                        </div>
                        <div class="customMiddle divObiturayFlowersOrderingShoppingCartMiddle">
                            <label class="lblFlowerItem">Price</label>                            
                        </div>
                        <div class="customRight divObiturayFlowersOrderingShoppingCartRight">
							&nbsp;
                        </div>
                        <div class="customFooter divObiturayFlowersOrderingShoppingCartFooter"></div>
                    </div>
                    <div id="divFlowerCartContent">
                        <asp:PlaceHolder runat="server" ID="phCartContent"></asp:PlaceHolder>
                    </div>
                    <asp:Panel runat="server" ID="panSubTotal">
                        <div id="divFlowerTotal">
                            <div class="customContainer divObiturayFlowersOrderingShoppingCartTotalContainer">
                                <div class="customLeft divObiturayFlowersOrderingShoppingCartTotalLeft">
                                    <label class="lblFlowerItem">Sub Total: </label>
                                </div>
                                <div class="customRight divObiturayFlowersOrderingShoppingCartTotalRight">
                                    <asp:Label runat="server" ID="lblCartSubTotal"></asp:Label>
                                </div>
                                <div class="customFooter divObiturayFlowersOrderingShoppingCartTotalFooter">
                                    <label>Before tax and service charge</label>
                                </div>
                            </div>
                        </div>
					</asp:Panel>
                </div>
                <div class="customContainer" id="divObiturayFlowersOrderingShoppingCartButtonsContainer">
                    <div class="customLeft" id="divObiturayFlowersOrderingShoppingCartButtonsLeft">
						<asp:Button runat="server" ID="cmdItemsBack" CssClass="green-button" Text="Continue Shopping" OnClick="cmdBack_Click"></asp:Button>

                    	<asp:Button runat="server" ID="cmdEmptyCart" Visible="false" CssClass="green-button" Text="Empty Cart" OnClick="cmdEmptyCart_Click" OnClientClick="javascript: return confirm('Do you what to empty cart?');"></asp:Button>
                    </div>
                    <div class="customRight" id="divObiturayFlowersOrderingShoppingCartButtonsRight">
                        <asp:Button runat="server" ID="cmdFlowerCheckout" Text="Checkout" CssClass="green-button" OnClick="cmdFlowerCheckout_Click"></asp:Button>
                    </div>
                    <br /> You will still have an opportunity on the next screen to edit your order.
                    <div class="customFooter" id="divObiturayFlowersOrderingShoppingCartButtonsFooter"></div>
                </div>
			</asp:Panel>
           	<asp:Panel runat="server" ID="panCheckOut" Visible="false">
            	<asp:UpdatePanel ID="ajaxBilling" runat="server">
			        <ContentTemplate>
                        <div class="obituaryHearder">
                            <asp:Label ID="lblDeliveryInfoHeader" runat="server" CssClass="lblCartSubHeader" Text="Delivery Info"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlDeliveryDate" CssClass="selectDeliveryDate" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlDeliveryDate_SelectedIndexChanged"></asp:DropDownList><label class="lblError">&nbsp;*</label>
                            <asp:CompareValidator ID="cfvDeliveryDate" runat="server" ControlToValidate="ddlDeliveryDate" Text="*" ErrorMessage="Fill in the delivery date" Display="Dynamic" Operator="NotEqual" ValueToCompare="-1" Type="String" ValidationGroup="OPC" CssClass="lblError"></asp:CompareValidator>
                            <br />
                            <div class="divError">
            					<asp:Label runat="server" ID="lblDeliveryError" Text="Important! The date you have chosen is several days in the future. Please verify that you have chosen correctly and then press the 'Proceed To Checkout' button." Visible="false"></asp:Label>
					        </div>
                            <label>This order usually can be delivered today if it is placed before 1:00PM recipients local time. Orders received after that cutoff will be delivered the following day unless the following day is Sunday in which case the delivery will be made on Monday.</label>
                        </div>
                        <div>
                            <br /><br />
                            <asp:TextBox ID="txtSpecialDelivery" runat="server" placeholder="Special Delivery Instructions" Width="511" TextMode="MultiLine" TabIndex="2" MaxLength="100"></asp:TextBox>
                            <br />
                            <label>(Maximum 100 characters)</label>
                            <br/>
                            <label>Please enter any information that would help us in the delivery of your order. Do not enter the address here as you will be asked for that information on the next page. Please do not enter special requests or other order related issues as the information that you enter here is for the delivery person only and is not reviewed by the orders department. We cannot accept requests for delivery at specific times of day.</label>
                        </div>
                        <div>
                        	<br /><br />
                            <asp:Label ID="lblCardMessageHeader" runat="server" Text="Add a personal message in the card"></asp:Label><%-- (optional)--%>
                            <br />
                            <asp:TextBox ID="txtCardMessage" runat="server" placeholder="Card Message" Width="511" TextMode="MultiLine" TabIndex="3" MaxLength="200"></asp:TextBox><label class="lblError">&nbsp;*</label>
                            <asp:RequiredFieldValidator ID="rfvCardMessage" runat="server" ControlToValidate="txtCardMessage" Display="Dynamic" Text="*" ErrorMessage="Fill card message" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                            <br />
                            <label>(Maximum 200 characters)</label>
                        </div>
                        <hr/>
                        <div class="obituaryHearder">
                            <asp:Label ID="lblPersonInfoHeader" runat="server" CssClass="lblCartSubHeader" Text="Customer Information"></asp:Label>
                        </div>
                                                
                        <div class="customContainer divObiturayFlowersHeaderCartHolderContainer">
                            <div class="customLeft divObiturayFlowersHeaderCartHolderLeft">
                                <div>
                                    <asp:TextBox ID="txtBillingFirstName" runat="server" placeholder="First Name" TabIndex="4" MaxLength="50"></asp:TextBox><label class="lblError">&nbsp;*</label>
                                    <asp:RequiredFieldValidator ID="rfvBillingFirstName" runat="server" ControlToValidate="txtBillingFirstName" Display="Dynamic" Text="*" ErrorMessage="Fill in the first name" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revBillingFirstName" ControlToValidate="txtBillingFirstName" Text="*" ErrorMessage="Invalid first name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC" CssClass="lblError"></asp:RegularExpressionValidator>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtBillingLastName" runat="server" placeholder="Last Name" TabIndex="5" MaxLength="50"></asp:TextBox><label class="lblError">&nbsp;*</label>
                                    <asp:RequiredFieldValidator ID="rfvBillingLastName" runat="server" ControlToValidate="txtBillingLastName" Display="Dynamic" Text="*" ErrorMessage="Fill in the last name" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revBillingLastName" ControlToValidate="txtBillingLastName" Text="*" ErrorMessage="Invalid last name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC" CssClass="lblError" />
                                </div>
                                <div>
                                    <asp:TextBox ID="txtBillingEmail" runat="server" placeholder="Email" TabIndex="6" MaxLength="50"></asp:TextBox><label class="lblError">&nbsp;*</label>
                                    <asp:RequiredFieldValidator ID="rfvBillingEmail" runat="server" ControlToValidate="txtBillingEmail" Display="Dynamic" Text="*" ErrorMessage="Fill in the email" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revBillingEmail" runat="server" Text="*" ErrorMessage="Invalid email format" ControlToValidate="txtBillingEmail" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$" Display="Dynamic" ValidationGroup="OPC" CssClass="lblError"></asp:RegularExpressionValidator>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtBillingPhoneNo" runat="server" placeholder="Phone Number" TabIndex="7" MaxLength="14"></asp:TextBox><label class="lblPaymentExample"> ex. (123) 456-7890 *</label>
                                    <asp:RequiredFieldValidator ID="BillingPhoneNoRequired" runat="server" Text="*" ErrorMessage="Fill in the phone" Display="Dynamic" ControlToValidate="txtBillingPhoneNo" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revBillingPhone" ControlToValidate="txtBillingPhoneNo" ErrorMessage="Invalid phone no format ex. (123) 456-7890" ValidationExpression="^\(?\d{3}\)? ?-? ?\d{3} ?-? ?\d{4}$" Text="*" Display="Dynamic" ValidationGroup="OPC" CssClass="lblError"></asp:RegularExpressionValidator>        
                                </div>
                                <div class="obituaryHearder divJustHidden">
                                    <asp:Label ID="lblBillingInfoHeader" runat="server" CssClass="lblCartSubHeader" Text="Billing Information"></asp:Label>
                                </div>
                            </div>
                            <div class="customRight divObiturayFlowersHeaderCartHolderRight">
                            	<div class="customContainer divObiturayFlowersHeaderCartHolderTextContainer">
                                    <div class="customLeft divObiturayFlowersHeaderCartHolderTextLeft">
                                        <img src="/Portals/_default/Skins/Obit/Images/Secure-Checkout.png" alt="Secure Checkout" />
                                    </div>
                                    <div class="customRight divObiturayFlowersHeaderCartHolderTextRight">
                                        <label>We adhere to strict industry security standards for payment processing.<br /><br />Please <a onclick="popup('http://theobituaries.ca/portals/_default/skins/obit/security.htm');return false;" href="_blank"><strong>click</strong></a> to review.</label>
                                    </div>
                                    <div class="customFooter divObiturayFlowersHeaderCartHolderTextFooter"></div>
                                </div>    
                            </div>
                            <div class="customFooter divObiturayFlowersHeaderCartHolderFooter"></div>
                        </div>
                                                    
                        <div>
                            <asp:TextBox ID="txtBillingAddress1" runat="server" placeholder="Address 1" Width="511" TabIndex="8" MaxLength="30"></asp:TextBox><label class="lblError">&nbsp;*</label>
                            <asp:RequiredFieldValidator ID="rfvBillingAddress1" runat="server" ControlToValidate="txtBillingAddress1" Display="Dynamic" Text="*" ErrorMessage="Fill in the address 1" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txtBillingAddress2" runat="server" placeholder="Address 2" Width="511" TabIndex="9" MaxLength="30"></asp:TextBox>
                        </div>
                        <div>
                            <asp:TextBox ID="txtBillingCity" runat="server" placeholder="City Or Town" Width="511" TabIndex="10" MaxLength="30"></asp:TextBox><label class="lblError">&nbsp;*</label>
                            <asp:RequiredFieldValidator ID="rfvBillingCity" runat="server" ControlToValidate="txtBillingCity" Display="Dynamic" Text="*" ErrorMessage="Fill in the city or town" ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                        </div>
                        <asp:UpdatePanel ID="ajaxBillingCountryProvince" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList runat="server" ID="ddlBillingProvince" DataTextField="ProvinceName" DataValueField="ProvinceID" Width="210" TabIndex="11"></asp:DropDownList>
                                </div>
                                <div>
                                    <asp:DropDownList runat="server" ID="ddlBillingCountry" DataTextField="CountryName" Width="210" DataValueField="CountryID" AutoPostBack="true" OnSelectedIndexChanged="ddlBillingCountry_SelectedIndexChanged" TabIndex="12"></asp:DropDownList>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtBillingPostalCode" runat="server" placeholder="Postal Code" Width="100" TabIndex="13" MaxLength="7"></asp:TextBox><label class="lblPaymentExample"> ex. <asp:Label runat="server" ID="lblPCEx" Text="A1A 1A1"></asp:Label> *</label>
                                    <asp:RequiredFieldValidator ID="rfvBillingPostalCode" runat="server" ControlToValidate="txtBillingPostalCode" Display="Dynamic" Text="*" ErrorMessage="Fill in the postal code ex. A1A 1A1"  ValidationGroup="OPC" CssClass="lblError"></asp:RequiredFieldValidator>
                                   <asp:RegularExpressionValidator runat="server" id="revPC" ControlToValidate="txtBillingPostalCode" Text="*" ErrorMessage="Invalid postal code format" ValidationExpression="^[A-Z]\d[A-Z][ ]\d[A-Z]\d$" Display="Dynamic" ValidationGroup="OPC" CssClass="lblError" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <hr/>
                        
                        <div class="divError"> 
                            <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Order has been filled out incorrectly" />
                                
                            <asp:Literal runat="server" ID="litFlowerError"></asp:Literal>
                        </div>
                        
                        <div class="divPaymentSubmit">
                            <asp:Button runat="server" ID="cmdGoToCredit" CssClass="green-button" Text="Proceed To Checkout" OnClick="cmdGoToCredit_Click" ValidationGroup="OPC"></asp:Button>
                        </div>
	                </ContentTemplate>
				</asp:UpdatePanel>
			</asp:Panel>
           	<asp:Panel runat="server" ID="panPlaceOrder" Visible="false">
            	<asp:UpdatePanel ID="ajaxPlaceOrder" runat="server">
					<ContentTemplate>
                    	<div runat="server" id="divThankYouOrderDetail">
                            <asp:Panel runat="server" ID="panThankYou" CssClass="divFlowerOrderThankYou" Visible="false">
                                <div class="divFlowerOrderThankYou teal" id="divOrderThankYou">
                                    <br />
                                    <label id="gothic1117" class="lblOrderThankYou">Your Order</label>
                                </div>
                                <div CssClass="divSignUpThankYouBody divSignUpBody">
                                    <label class="lblFontSize16">Order Number: <asp:Label runat="server" ID="lblOrderNumber"></asp:Label></label>
                                    <br/><br/>
                                    <label class="lblFontSize14">Thank you for your order.</label>
                                    <br/><br/>
                                    <label class="lblFontSize14">An email with your order details has been sent to you.</label>
                                </div>
                            </asp:Panel>
                            
							<div class="print-pad customContainer divObiturayFlowersOrderingShoppingCartInforContainer" id="divOrderPrintPad">
                                <hr />
                                <asp:Panel runat="server" ID="panPlaceOrderAddress">
                                    <div class="customLeft divObiturayFlowersOrderingShoppingCartInforLeft">
                                        <div>
                                            <label><strong>Billed To:</strong></label>
                                        </div>
                                        <div>
                                            <asp:Literal runat="server" ID="litPlaceOrderAddress"></asp:Literal>
                                        </div>
                                    </div>
                                </asp:Panel>                            
                                <div class="customRight divObiturayFlowersOrderingShoppingCartInforRight">
                                    <asp:Panel runat="server" ID="panPlaceOrderDeliveryDate">
                                        <label><strong>Delivery Date:</strong>
                                        <asp:label runat="server" ID="lblPlaceOrderDeliveryDate"></asp:label></label>
                                    </asp:Panel> 
                                    <asp:Panel runat="server" ID="panPlaceOrderSpecialDelivery">
                                        <label><strong>Special Delivery:</strong>
                                        <br />
                                        <asp:Literal runat="server" ID="litPlaceOrderSpecialDelivery"></asp:Literal>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panPlaceOrderCardMessage">
                                        <label><strong>Card Message:</strong></label>
                                        <br />
                                        <asp:Literal runat="server" ID="litPlaceOrderCardMessage"></asp:Literal>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panPlaceOrderEdit" CssClass="divPlaceOrderEdit" Visible="false">
                                        <br /><br />
                                        <asp:Button runat="server" ID="cmdEdit" CssClass="green-button" Text="Edit" OnClick="cmdGoToFlowerCheckout_Click"></asp:Button>
                                    </asp:Panel>                                
                                </div>
                                <div class="customFooter divObiturayFlowersOrderingShoppingCartInforFooter"></div>
                                <hr width="94%"/>
                                <div class="customContainer divObiturayFlowersOrderingShoppingCartInforContainer" id="divOrderItems">
                                    <div class="customMiddle divObiturayFlowersOrderingShoppingCartInforDetailsLeft">
                                        <label><strong>Item</strong></label>
                                        <hr />
                                        <asp:Literal runat="server" ID="litFlowerShoppingCartInforItems"></asp:Literal>
                                    </div>
                                    <div class="customMiddle divObiturayFlowersOrderingShoppingCartInforDetailsMiddle">
                                        <label><strong>Seller</strong></label>
                                        <hr width="100%" />
                                        <asp:Literal runat="server" ID="litFlowerShoppingCartInforItemsSeller"></asp:Literal>
                                    </div>
                                    <div class="customMiddle divObiturayFlowersOrderingShoppingCartInforDetailsMiddle2">
                                        <hr width="100%" />
                                    </div>
                                    <div class="customRight divObiturayFlowersOrderingShoppingCartInforDetailsRight">
                                        <label><strong>Unit Price</strong></label>
                                        <hr width="94%" />
                                        <asp:Literal runat="server" ID="litFlowerShoppingCartInforItemsPrice"></asp:Literal>
                                        <br />
                                    </div>
                                    <div class="customFooter divObiturayFlowersOrderingShoppingCartInforDetailsFooter"></div>
								</div>
                                                  
                                <div class="customLeft divObiturayFlowersOrderingShoppingCartInforTotalsLeft">&nbsp;</div>
                                <div class="customMiddle divObiturayFlowersOrderingShoppingCartInforTotalsMiddle">
                                    <label>Sub-Total:
                                    <br />
                                    Service Charge:
                                    <br />
                                    Taxes:
                                    <br /></label>
                                </div>
                                <div class="customRight divObiturayFlowersOrderingShoppingCartInforTotalsRight">
                                    <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
                                    <br />
                                    <asp:Label runat="server" ID="lblServiceCharage"></asp:Label>
                                    <br />
                                    <asp:Label runat="server" ID="lblTaxes"></asp:Label>
                                </div>                                
                                <div class="customFooter divObiturayFlowersOrderingShoppingCartInforTotalsFooter"></div>
                                <div class="customLeft divObiturayFlowersOrderingShoppingCartInforTotalsLeft">&nbsp;</div>
                                <div class="customMiddle divObiturayFlowersOrderingShoppingCartInforTotalsMiddle">
                                    <label>Total:</label>
                                </div>
                                <div class="customRight divObiturayFlowersOrderingShoppingCartInforTotalsRight">
                                    <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                </div>
                                <div class="customFooter divObiturayFlowersOrderingShoppingCartInforTotalsFooter"></div>
                            </div>
                            
                            <%--<div class="customContainer divObiturayFlowersOrderingShoppingCartInforContainer">
                                <div class="customLeft divObiturayFlowersOrderingShoppingCartInforLeft">
                                    <asp:Panel runat="server" ID="panPlaceOrderDeliveryDate" CssClass="customContainer">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartInforFieldLeft">
                                            <label class="lblFlowerItem">Delivery Date: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartInforFieldRight">
                                            <asp:label runat="server" ID="lblPlaceOrderDeliveryDate"></asp:label>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartInforFieldFooter"></div>
                                    </asp:Panel>--%>
                                  <%--  <asp:Panel runat="server" ID="panPlaceOrderSpecialDelivery" CssClass="customContainer divObiturayFlowersOrderingShoppingCartInforFieldContainer">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartInforFieldLeft">
                                            <label id="gothic1118" class="lblFlowerItem">Special Delivery: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartInforFieldRight">
                                            <asp:Literal runat="server" ID="litPlaceOrderSpecialDelivery"></asp:Literal>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartInforFieldFooter"></div>
                                    </asp:Panel>--%>
                                  <%--  <asp:Panel runat="server" ID="panPlaceOrderCardMessage" CssClass="customContainer divObiturayFlowersOrderingShoppingCartInforFieldContainer">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartInforFieldLeft">
                                            <label id="gothic1115" class="lblFlowerItem">Card Message: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartInforFieldRight">
                                            <asp:Label runat="server" ID="litPlaceOrderCardMessage"></asp:Label>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartInforFieldFooter"></div>
                                    </asp:Panel>--%>
                                    <%--<asp:Panel runat="server" ID="panPlaceOrderAddress" CssClass="customContainer divObiturayFlowersOrderingShoppingCartInforFieldContainer">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartInforFieldLeft">
                                            <label class="lblFlowerItem">Billing Information: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartInforFieldRight">
                                            <asp:Literal runat="server" ID="litPlaceOrderAddress"></asp:Literal>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartInforFieldFooter"></div>
                                    </asp:Panel>--%>
                                   <%-- <asp:Panel runat="server" ID="panPlaceOrderEdit" CssClass="divPlaceOrderEdit">
                                        <asp:Button runat="server" ID="cmdEdit" CssClass="green-button" Text="Edit" OnClick="cmdGoToFlowerCheckout_Click"></asp:Button>
                                    </asp:Panel>
                                </div>--%>
                                <%--<div class="customRight divObiturayFlowersOrderingShoppingCartInforRight">
                                    <div class="customContainer">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartTotalLeft">
                                            <label class="lblFlowerItem">Sub Total: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartTotalRight">
                                            <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartTotalFooter"></div>
                                    </div>
                                    <div class="customContainer divObiturayFlowersOrderingShoppingCartTotalContainer divObiturayFlowersOrderingShoppingCartPaymentTotal">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartTotalLeft">
                                            <label class="lblFlowerItem">Service Charge: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartTotalRight">
                                            <asp:Label runat="server" ID="lblServiceCharage"></asp:Label>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartTotalFooter"></div>
                                    </div>
                                    <div class="customContainer divObiturayFlowersOrderingShoppingCartTotalContainer divObiturayFlowersOrderingShoppingCartPaymentTotal">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartTotalLeft">
                                            <label class="lblFlowerItem">Taxes: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartTotalRight">
                                            <asp:Label runat="server" ID="lblTaxes"></asp:Label>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartTotalFooter"></div>
                                    </div>                      
                                    <div class="customContainer divObiturayFlowersOrderingShoppingCartTotalContainer divObiturayFlowersOrderingShoppingCartPaymentTotal">
                                        <div class="customLeft divObiturayFlowersOrderingShoppingCartTotalLeft">
                                            <label class="lblFlowerItem">Total: </label>
                                        </div>
                                        <div class="customRight divObiturayFlowersOrderingShoppingCartTotalRight">
                                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                        </div>
                                        <div class="customFooter divObiturayFlowersOrderingShoppingCartTotalFooter"></div>
                                    </div>
                                </div>
                                <div class="customFooter divObiturayFlowersOrderingShoppingCartInforFooter"></div>
                            </div>--%>
                        </div>            
                        <asp:Panel runat="server" ID="panCreditCard">
                        	<hr/>
                            <asp:UpdatePanel ID="ajaxCreditCard" runat="server">
                                <ContentTemplate>
                                    <div class="obituaryHearder">
                                        <asp:Label ID="lblCCHeader" runat="server" Text="Credit Card"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:DropDownList runat="server" ID="ddlBillingCreditCardType" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlBillingCreditCardType_SelectedIndexChanged">
                                            <asp:ListItem Text="Credit Card Type" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="American Express" Value="AX"></asp:ListItem>
                                            <asp:ListItem Text="Visa" Value="VI"></asp:ListItem>
                                            <asp:ListItem Text="MasterCard" Value="MC"></asp:ListItem>
                                        </asp:DropDownList><label class="lblError">&nbsp;*</label>
                                        <asp:CompareValidator ID="cfvBillingCreditCardType" runat="server" ControlToValidate="ddlBillingCreditCardType" Text="*" ErrorMessage="Fill in the credit card type" Display="Dynamic" Operator="NotEqual" ValueToCompare="-1" Type="String" ValidationGroup="Payment" CssClass="lblError"></asp:CompareValidator>&nbsp;&nbsp;&nbsp;
                                        <img alt="Obituary preview" src="/Portals/_default/Skins/Obit/Images/visa_mastercard_logo2.jpeg" width="157"></img>
                                    </div>
                                    
									<div class="customContainer divObiturayFlowersHeaderCartHolderContainer">
                                        <div class="customLeft divObiturayFlowersHeaderCartHolderLeft">
                                            <div>
                                                <asp:TextBox ID="txtBillingNameOnCard" runat="server" placeholder="Name on Card" TabIndex="2" autocomplete="off"></asp:TextBox><label class="lblError">&nbsp;*</label>
                                                <asp:RequiredFieldValidator ID="rfvBillingNameOnCard" runat="server" ControlToValidate="txtBillingNameOnCard" Display="Dynamic" Text="*" ErrorMessage="Fill in the name on card" ValidationGroup="Payment" CssClass="lblError"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" id="revBillingNameOnCard" ControlToValidate="txtBillingNameOnCard" Text="*" ErrorMessage="Invalid name on card format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\&\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="Payment" CssClass="lblError" />
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtBillingCreditCardNumber" runat="server" placeholder="Credit Card Number" autocomplete="off" TabIndex="3" MaxLength="16"></asp:TextBox><label class="lblError">&nbsp;*</label>
                                                <asp:RequiredFieldValidator ID="rfvBillingCreditCardNumber" runat="server" ControlToValidate="txtBillingCreditCardNumber" Display="Dynamic" Text="*" ErrorMessage="Fill in the credit card number" ValidationGroup="Payment" CssClass="lblError"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revBillingCreditCardNumber" runat="server" ControlToValidate="txtBillingCreditCardNumber" Display="Dynamic" Text="*" ErrorMessage="Not valid credit card number" ValidationExpression="^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$" ValidationGroup="Payment" CssClass="lblError"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="customRight divObiturayFlowersHeaderCartHolderRight divObiturayFlowersHeaderCCCartHolderRight">
                                            <div class="customContainer divObiturayFlowersHeaderCartHolderTextContainer">
                                                <div class="customLeft divObiturayFlowersHeaderCartHolderTextLeft">
                                                    <img src="/Portals/_default/Skins/Obit/Images/Secure-Checkout.png" alt="Secure Checkout" />
                                                </div>
                                                <div class="customRight divObiturayFlowersHeaderCartHolderTextRight">
                                                    <label>We adhere to strict industry security standards for payment processing.<br /><br />Please <a onclick="popup('http://theobituaries.ca/portals/_default/skins/obit/security.htm');return false;" href="_blank"><strong>click</strong></a> to review.</label>
                                                </div>
                                                <div class="customFooter divObiturayFlowersHeaderCartHolderTextFooter"></div>
                                            </div>    
                                        </div>
                                        <div class="customFooter divObiturayFlowersHeaderCartHolderFooter"></div>
                                    </div>
                                    
                                    <div class="obituaryHearder">
                                        <asp:Label ID="lblExpiryDateHeader" runat="server" Text="EXPIRY DATE"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:DropDownList runat="server" ID="ddlExpiryMonth" Width="58" TabIndex="4">
                                            <asp:ListItem Text="MM" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="08"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        </asp:DropDownList><label class="lblError">&nbsp;*</label>
                                        <asp:CompareValidator ID="cfvExpiryMonth" runat="server" ControlToValidate="ddlExpiryMonth" Text="*" ErrorMessage="Fill in the expiry month" Display="Dynamic" Operator="NotEqual" ValueToCompare="-1" Type="String" ValidationGroup="Payment" CssClass="lblError"></asp:CompareValidator>
                                        
                                        <asp:DropDownList runat="server" ID="ddlExpiryYear" Width="70" TabIndex="5"></asp:DropDownList><label class="lblError">&nbsp;*</label>
                                        <asp:CompareValidator ID="cfvExpiryYear" runat="server" ControlToValidate="ddlExpiryYear" Text="*" ErrorMessage="Fill in the expiry year" Display="Dynamic" Operator="NotEqual" ValueToCompare="-1" Type="String" ValidationGroup="Payment" CssClass="lblError"></asp:CompareValidator>
                                        
                                        <asp:TextBox ID="txtBillingSecurityCode" runat="server" placeholder="Security Code" autocomplete="off" TabIndex="6" MaxLength="3" Width="82"></asp:TextBox><label class="lblError">&nbsp;*</label> <a onclick="popup('http://theobituaries.ca/portals/_default/skins/obit/cvv2.htm');return false;" href="_blank">What is the CVV2 security code?</a>
                                        <asp:RequiredFieldValidator ID="rfvBillingSecurityCode" runat="server" ControlToValidate="txtBillingSecurityCode" Display="Dynamic" Text="*" ErrorMessage="Fill in the security code" ValidationGroup="Payment" CssClass="lblError"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cfvBillingSecurityCode" runat="server" ControlToValidate="txtBillingSecurityCode" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" Text="*" ErrorMessage="Security code is only numeric" ValidationGroup="Payment" CssClass="lblError"></asp:CompareValidator>
                                    </div>
                                    <hr/>
                                    <div class="divError"> 
                                        <asp:ValidationSummary ID="vsPayment" runat="server" CssClass="vsRegistration" ValidationGroup="Payment" DisplayMode="List" HeaderText="Payment has been filled out incorrectly" />
                                            
                                        <asp:Literal runat="server" ID="litPaymentError"></asp:Literal>
                                    </div>
                                    <div>
                                        <asp:CheckBox runat="server" ID="chkAgreeTerms"></asp:CheckBox>
                                        <label>I agree to the <a href="http://www.floristone.com/customer.cfm" target="_blank">terms of use</a> &amp; <a href="/commercialtermsofuse.aspx" target="_blank">commercial terms</a></label>
                                    </div>
                                    <div>
                                        <label>theobituaries.ca does not retain credit card information.</label>
                                    </div>
                                    
                                    <div class="divPaymentSubmit">
                                         <asp:Button runat="server" ID="cmdGoToFlowerCheckout" CssClass="green-button" Text="< Customer Information" OnClick="cmdGoToFlowerCheckout_Click" TabIndex="7"></asp:Button>
                                        <asp:Button ID="cmdPayment" runat="server" Text="Place Order" OnClick="cmdPayment_Click" ValidationGroup="Payment" CssClass="green-button" TabIndex="8"></asp:Button>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                	</ContentTemplate>
				</asp:UpdatePanel>
			</asp:Panel>
		</asp:Panel>
        
		<asp:HiddenField ID="hfPersonId" runat="server"></asp:HiddenField>
		<asp:HiddenField ID="hfObituatyId" runat="server"></asp:HiddenField>
		<asp:HiddenField ID="hfFHPID" runat="server"></asp:HiddenField>
    </ContentTemplate>
</asp:UpdatePanel>

<uc1:FlowerFooter runat="server" ID="FlowerFooter" />