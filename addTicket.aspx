<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addTicket.aspx.cs" Inherits="addTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Add Ticket
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="tickets.aspx">Ticket</a></li>
                <li class="active">Add Ticket</li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <section>
                    <!-- Default box -->
                    <div class="box">
                        <div class="box-body">

                            <!-- BOx -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Add Ticket</h3>

                                </div>
                                <div class="box-body">
                                    <div class="row table-responsive" id="mainDiv" runat="server">

                                        <asp:Panel ID="Panel3" runat="server" DefaultButton="searchReturnItem">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                        <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th width="20%";>Search By</th>
                                                <th>Barcode/SalesID</th>
                                                <th>Search</th>
                                            </tr>
                                            <tr>
                                                <td width="20%";><asp:RadioButtonList ID="searchBy" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonWidth ">
                                                    <asp:ListItem Value="1" Selected="True"> Barcode</asp:ListItem>
                                                    <asp:ListItem Value="2"> SalesID</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                                <td><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td><asp:LinkButton ID="searchReturnItem" runat="server" OnClick="searchReturnItem_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                            </tr>
                                        </table>
                                    </ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>
                                    </div>
                                    <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div class="row" id="showItem" runat="server" visible="false">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <th>Serchtext</th>
                                                <th colspan="6">
                                                    <asp:Label ID="searchDisplay" runat="server" Text=''></asp:Label></th>
                                            </tr>
                                            <tr>
                                                <th>Barcode</th>
                                                <th>Sales ID</th>
                                                <th>Item Category</th>
                                                <th>Brand</th>
                                                <th>Virtual location</th>
                                                <th>Image 1</th>
                                                <th>Add</th>
                                            </tr>
                                            <asp:Repeater ID="rptShowItem" runat="server" OnItemDataBound="rptShowItem_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="salesid" runat="server" Visible="false" Text='<%# Eval("sid").ToString() %>'></asp:Label>
                                                           
                                                            <asp:Label ID="StockupID" runat="server" Visible="false" Text='<%# Eval("StockupID").ToString() %>'></asp:Label>
                                                            <asp:Label ID="Barcode" runat="server" Text='<%# Eval("BarcodeNo").ToString() %>'></asp:Label>

                                                            <asp:Label ID="salesAbwno" runat="server" Visible="false" Text='<%# Eval("salesAbwno").ToString() %>'></asp:Label>
                                                            <asp:Label ID="courierName" runat="server" Visible="false" Text='<%# Eval("courierName").ToString() %>'></asp:Label>
                                                            <asp:Label ID="salesDateTime" runat="server" Visible="false" Text='<%# Eval("salesDateTime").ToString() %>'></asp:Label>
                                                            <asp:Label ID="soldBy" runat="server" Visible="false" Text='<%# Eval("soldBy").ToString() %>'></asp:Label>
                                                            <asp:Label ID="dispatchtimestamp" runat="server" Visible="false" Text='<%# Eval("dispatchtimestamp").ToString() %>'></asp:Label>
                                                            <asp:Label ID="dispatchedBy" runat="server" Visible="false" Text='<%# Eval("dispatchedBy").ToString() %>'></asp:Label>
                                                            <asp:Label ID="custname" runat="server" Visible="false" Text='<%# Eval("custname").ToString() %>'></asp:Label>
                                                            <asp:Label ID="invid" runat="server" Visible="false" Text='<%# Eval("invid").ToString() %>'></asp:Label>
                                                            <asp:Label ID="paymentMode" runat="server" Visible="false" Text='<%# Eval("paymentMode").ToString() %>'></asp:Label>
                                                            <asp:Label ID="returnAbwno" runat="server" Visible="false" Text='<%# Eval("returnAbwno").ToString() %>'></asp:Label>
                                                            <asp:Label ID="retCourier" runat="server" Visible="false" Text='<%# Eval("retCourier").ToString() %>'></asp:Label>
                                                            <asp:Label ID="returntimestamp" runat="server" Visible="false" Text='<%# Eval("returntimestamp").ToString() %>'></asp:Label>
                                                            <asp:Label ID="returnedBy" runat="server" Visible="false" Text='<%# Eval("returnedBy").ToString() %>'></asp:Label>
                                                            <asp:Label ID="remarks" runat="server" Visible="false" Text='<%# Eval("remarks").ToString() %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="salesidgivenbyvloc" runat="server" Text='<%# Eval("salesidgivenbyvloc").ToString() %>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="ItemCategory" runat="server" Text='<%# Eval("ItemCategory").ToString() %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="C1Name" runat="server" Text='<%# Eval("C1Name").ToString() %>'></asp:Label>
                                                        </td>
                                                         <td>
                                                            <asp:Label ID="Location" runat="server" Text='<%# Eval("Location").ToString() %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="image1" runat="server" Visible="false" Text='<%# Eval("image1").ToString() %>' ></asp:Label>
                                                            <asp:image ID="image1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="addNewTicket" runat="server" CssClass="btn btn-sm btn-danger" OnClick="addNewTicket_Click"><i class="fa fa-plus"></i> Add New Ticket</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <div class="row" id="noData" runat="server" visible="false">
                                        NO DATA
                                    </div>


                                    <div runat="server" id="markReturn" visible="false">

                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box">
                                                    <div class="box-body table-responsive">
                                                        <div class="col-xs-12">
                                                            <table class="table table-bordered table-striped table-hover">
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="displayBarcode" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Item Category</th>
                                                                    <td>
                                                                        <asp:Label ID="displayCategory" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                               
                                                                <tr>
                                                                    <th>Brand</th>
                                                                    <td>
                                                                        <asp:Label ID="DC1Name" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Image 1</th>
                                                                    <td><asp:image ID="displayimage1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>
                                                            
                                                                <tr>
                                                                    <th>Sales ID</th>
                                                                    <td>
                                                                        <asp:Label ID="Dsalesidgivenbyvloc" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                      <th>Sales ABW No
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="DsalesAbwno" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                    
                                                                     <th>Sales Date</th>
                                                                    <td>
                                                                        <asp:Label ID="DsalesDateTime" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Sold By</th>
                                                                    <td>
                                                                        <asp:Label ID="DsoldBy" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                     <th>Dispatch Date</th>
                                                                    <td>
                                                                        <asp:Label ID="Ddispatchtimestamp" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                     <th>Dispatch Courier</th>
                                                                    <td>
                                                                        <asp:Label ID="DcourierName" runat="server" Text=''></asp:Label></td>
                                                                   

                                                                </tr>
                                                               
                                                                <tr>
                                                                    <th>Dispatched By</th>
                                                                    <td>
                                                                        <asp:Label ID="DdispatchedBy" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Customer</th>
                                                                    <td>
                                                                        <asp:Label ID="Dcustname" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Returned By</th>
                                                                    <td>
                                                                        <asp:Label ID="DreturnedBy" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Retuen Date</th>
                                                                    <td>
                                                                        <asp:Label ID="Dreturntimestamp" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Return ABW No</th>
                                                                    <td>
                                                                        <asp:Label ID="DreturnAbwno" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Retuen Courier</th>
                                                                    <td>
                                                                        <asp:Label ID="DretCourier" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                
                                                               
                                                                <tr>
                                                                    <th>Remarks </th>
                                                                    <td>
                                                                        <asp:Label ID="Dremarks" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <th>Invoice No.</th>
                                                                    <td>
                                                                        <asp:Label ID="Dinvid" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                   
                                                                </tr>
                                                               
                                                                <tr>
                                                                     <th>Payment Mode</th>
                                                                    <td>
                                                                        <asp:Label ID="DpaymentMode" runat="server" Text=''></asp:Label>
                                                                                              </td>
                                                                    <th>Virtual Location</th>
                                                                    <td>
                                                                        <asp:Label ID="DLocation" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Return Image 1</th>
                                                                   <td><asp:Label ID="rImage1Link" runat="server" Text=""></asp:Label></br> 
                                                                       <asp:image ID="rImage1" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                    <th>Return Image 2</th>
                                                                    <td><asp:Label ID="rImage2Link" runat="server" Text=""></asp:Label></br> 
                                                                        <asp:image ID="rImage2" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Return Image 3</th>
                                                                   <td><asp:Label ID="rImage3Link" runat="server" Text=""></asp:Label></br>
                                                                       <asp:image ID="rImage3" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                    <th>Return Image 4</th>
                                                                    <td><asp:Label ID="rImage4Link" runat="server" Text=""></asp:Label></br>
                                                                        <asp:image ID="rImage4" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Return Image 5</th>
                                                                   <td><asp:Label ID="rImage5Link" runat="server" Text=""></asp:Label></br>
                                                                       <asp:image ID="rImage5" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                    <th>Return Image 6</th>
                                                                    <td><asp:Label ID="rImage6Link" runat="server" Text=""></asp:Label></br>
                                                                        <asp:image ID="rImage6" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Return Image 7</th>
                                                                   <td><asp:Label ID="rImage7Link" runat="server" Text=""></asp:Label></br>
                                                                       <asp:image ID="rImage7" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                    <th>Return Image 8</th>
                                                                    <td><asp:Label ID="rImage8Link" runat="server" Text=""></asp:Label></br>
                                                                        <asp:image ID="rImage8" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Return Image 9</th>
                                                                   <td><asp:Label ID="rImage9Link" runat="server" Text=""></asp:Label></br>
                                                                       <asp:image ID="rImage9" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                    <th>Return Image 10</th>
                                                                    <td><asp:Label ID="rImage10Link" runat="server" Text=""></asp:Label></br>
                                                                        <asp:image ID="rImage10" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>

                                                                <tr>
                                                                    <th>Return Video 1</th>
                                                                   <td><asp:Label ID="rVideo1Link" runat="server" Text=""></asp:Label></br>
                                                                       <iframe id="rVideo1" runat="server"
                                                                         width = "250" 
                                                                         height = "200"                                                                          
                                                                         frameborder  ="0" 
                                                                         >
                                                                      </iframe></td>
                                                                    <th>Return Video 2</th>
                                                                    <td>
                                                                        <asp:Label ID="rVideo2Link" runat="server" Text=""></asp:Label></br>
                                                                        <iframe id="rVideo2" runat="server"
                                                                         width = "250" 
                                                                         height = "200"                                                                          
                                                                         frameborder  ="0" 
                                                                         >
                                                                      </iframe></td>
                                                                </tr>


                                                            </table>

                                                            <asp:Label ID="displayStockupID" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="displayArchiveStockupID" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="displaysalesid" runat="server" Text="" Visible="false"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Ticket No. </label>
                                                                <asp:TextBox ID="ticketNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Issue Date </label>
                                                                <asp:TextBox ID="issueDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Description </label>
                                                                <asp:TextBox ID="description" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                      
                                                        
                                                       
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnAddTicket" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Add Ticket" OnClick="btnAddTicket_Click" />
                                                                <asp:Button ID="btnSave" class="btn btn-success pull-right btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </section>

            </ContentTemplate>
            <Triggers>
				<asp:PostBackTrigger ControlID="btnSave" />
                
			</Triggers>

        </asp:UpdatePanel>
    </div>
</asp:Content>

