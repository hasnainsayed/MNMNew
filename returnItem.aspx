<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="returnItem.aspx.cs" Inherits="returnItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
     .RadioButtonWidth label {  margin-right:10px; }  
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Record Return Item
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a href="returnItem.aspx">Record Return Item</a></li>
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
                                    <h3 class="box-title">Record Return Item</h3>

                                </div>
                                <div class="box-body">
                                    <div class="row table-responsive" id="mainDiv" runat="server">
                                        <asp:Panel ID="Panel3" runat="server" DefaultButton="searchSoldItem">
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
                                                    <asp:ListItem Value="3"> Website & Barcode</asp:ListItem>
                                                    <asp:ListItem Value="4"> Website & SalesID</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                                <td><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                
                                                <td><asp:LinkButton ID="searchSoldItem" runat="server" OnClick="searchSoldItem_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
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
                                                <th>Return</th>
                                            </tr>
                                            <asp:Repeater ID="rptShowItem" runat="server" OnItemDataBound="rptShowItem_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="salesid" runat="server" Visible="false" Text='<%# Eval("sid").ToString() %>'></asp:Label>
                                                            <asp:Label ID="ArchiveStockupID" runat="server" Visible="false" Text='<%# Eval("ArchiveStockupID").ToString() %>'></asp:Label>
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
                                                            <asp:LinkButton ID="return" runat="server" CssClass="btn btn-sm btn-danger" OnClick="return_Click"><i class="fa fa-arrow-left"></i> Mark Return</asp:LinkButton>
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
                                                                </tr>
                                                                <tr>
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
                                                                </tr>
                                                                <tr>
                                                                    <th>Image 1</th>
                                                                    <td><asp:image ID="displayimage1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales ID</th>
                                                                    <td>
                                                                        <asp:Label ID="Dsalesidgivenbyvloc" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales ABW No
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="DsalesAbwno" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Courier</th>
                                                                    <td>
                                                                        <asp:Label ID="DcourierName" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales Date</th>
                                                                    <td>
                                                                        <asp:Label ID="DsalesDateTime" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
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
                                                                </tr>
                                                                <tr>
                                                                    <th>Dispatched By</th>
                                                                    <td>
                                                                        <asp:Label ID="DdispatchedBy" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <th>Customer</th>
                                                                    <td>
                                                                        <asp:Label ID="Dcustname" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
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
                                                                </tr>
                                                                <tr>
                                                                    <th>Virtual Location</th>
                                                                    <td>
                                                                        <asp:Label ID="DLocation" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Physical Location</th>
                                                                    <td>
                                                                        <asp:Label ID="DphyLocation" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>


                                                            </table>

                                                            <asp:Label ID="displayStockupID" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="displayArchiveStockupID" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="displaysalesid" runat="server" Text="" Visible="false"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Status </label>
                                                                <asp:DropDownList ID="returnStatus" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="RFL">RFL</asp:ListItem>
                                                                    <asp:ListItem Value="NRM">Minor</asp:ListItem>
                                                                    <asp:ListItem Value="NRD">Damaged</asp:ListItem>
                                                                    <asp:ListItem Value="NRR">Retail</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Courier Service </label>
                                                                <asp:DropDownList ID="courier" runat="server" CssClass="form-control select2" DataTextField="courierName" DataValueField="courierId"></asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Return AWB No. </label>
                                                                <asp:TextBox ID="awbNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Return Reason </label>
                                                                <asp:DropDownList ID="reasons" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="1">COD Return</asp:ListItem>
                                                                    <asp:ListItem Value="2">Customer Return</asp:ListItem>
                                                                    <asp:ListItem Value="3">Customer Cancellation</asp:ListItem>
                                                                    <asp:ListItem Value="4">Others</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Return Remarks </label>
                                                                <asp:TextBox ID="remarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                         

                                                        
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image1 </label>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image2</label>
                                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image3</label>
                                                                <asp:FileUpload ID="FileUpload3" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image4</label>
                                                                <asp:FileUpload ID="FileUpload4" runat="server" />
                                                            </div>

                                                        </div>
                                                           

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image5</label>
                                                                <asp:FileUpload ID="FileUpload5" runat="server" />
                                                            </div>

                                                        </div>

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image6</label>
                                                                <asp:FileUpload ID="FileUpload6" runat="server" />
                                                            </div>

                                                        </div>

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image7</label>
                                                                <asp:FileUpload ID="FileUpload7" runat="server" />
                                                            </div>

                                                        </div>

                                                             <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image8</label>
                                                                <asp:FileUpload ID="FileUpload8" runat="server" />
                                                            </div>

                                                        </div>

                                                             <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image9</label>
                                                                <asp:FileUpload ID="FileUpload9" runat="server" />
                                                            </div>

                                                        </div>

                                                             <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image10</label>
                                                                <asp:FileUpload ID="FileUpload10" runat="server" />
                                                            </div>

                                                        </div>

                                                            <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Video 1</label>
                                                                <%--<asp:FileUpload ID="FileUploadV1" runat="server" />--%>
                                                                <asp:TextBox ID="rVideo1Link" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                            <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Video 2</label>
                                                                <%--<asp:FileUpload ID="FileUploadV2" runat="server" />--%>
                                                                <asp:TextBox ID="rVideo2Link" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                         </div>
                                                       
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnReturnList" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to List" OnClick="btnReturnList_Click" />
                                                                <asp:Button ID="btnSave" class="btn btn-success pull-right btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="latest20">
                                         <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Returned Items</h3>

                                </div>
                                <div class="box-body">
                                    <table class="table table-bordered table-stripped dtSearch">
                                        <thead>
                                        <tr>
                                            <th>Barcode</th>
                                            <th>Return Datetime</th>
                                            <th>Virtual Location</th>
                                            <th>Sales ID</th>
                                            <th>Courier</th>                                          
                                            <th>Returned By</th>                                            
                                            <th>ABW No</th>
                                            <th>Reason</th>
                                            <th>Remarks</th>                                            
                                        </tr>
                                              </thead>
                                        <tbody>
                                        <asp:Repeater ID="rpt_Return" runat="server" >
                                           <ItemTemplate>
                                               <tr>
                                                   <td><%# Eval("BarcodeNo").ToString() %></td>
                                                   <td><%# Eval("returntimestamp").ToString() %></td>
                                                   <td><%# Eval("Location").ToString() %></td>
                                                   <td><%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                   <td><%# Eval("courierName").ToString() %></td>
                                                   <td><%# Eval("username").ToString() %></td>
                                                   <td><%# Eval("returnAbwno").ToString() %></td>
                                                   <td><%# Eval("reasons").ToString() %></td>
                                                   <td><%# Eval("remarks").ToString() %></td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                        </tbody>
                                    </table>
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

