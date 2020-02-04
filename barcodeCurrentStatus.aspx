<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="barcodeCurrentStatus.aspx.cs" Inherits="barcodeCurrentStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Barcode Current Status
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Barcode Current Status</li>
        </ol>
    </section>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Barcode Current Status</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   <asp:Panel ID="Panel3" runat="server" DefaultButton="getStatus">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                            <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th>Barcode</th>
                                                <th>Get Status</th>
                                            </tr>
                                            <tr>
                                               
                                                <td><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td><asp:LinkButton ID="getStatus" runat="server" OnClick="getStatus_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Get Status</asp:LinkButton></td>
                                            </tr>
                                        </table>
                                     </ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>

                        <div runat="server" id="showData" class=" table-responsive">
                            
                               <table class="table table-bordered table-stripped ">
                               <tr>
                                    <th width="10%!important">BarcodeNo</th>
                                    <td width="20%!important"><asp:Label ID="BarcodeNo" runat="server" Text=""></asp:Label></td>
                                    <th width="10%!important">SystemDate</th>
                                    <td width="20%!important"><asp:Label ID="SystemDate" runat="server" Text=""></asp:Label></td>
                                    <th width="10%!important">Status</th>
                                    <td width="20%!important"><asp:Label ID="Status" runat="server" Text=""></asp:Label></td>
                               </tr>
                                <tr>
                                    <th>StyleCode</th>
                                    <td><asp:Label ID="StyleCode" runat="server" Text=""></asp:Label></td>
                                    <th>SKU</th>
                                    <td><asp:Label ID="SKU" runat="server" Text=""></asp:Label></td>
                                    <th>MRP</th>
                                    <td><asp:Label ID="mrp" runat="server" Text=""></asp:Label></td>
                               </tr>

                               <tr>
                                    <th>LotNo</th>
                                    <td><asp:Label ID="LotNo" runat="server" Text=""></asp:Label></td>
                                    <th>Brand</th>
                                    <td><asp:Label ID="Brand" runat="server" Text=""></asp:Label></td>
                                    <th>Title</th>
                                    <td><asp:Label ID="Titles" runat="server" Text=""></asp:Label></td>
                               </tr>
                                 <tr>
                                    <th>Size</th>
                                    <td><asp:Label ID="Size" runat="server" Text=""></asp:Label></td>
                                    <th>Vertical</th>
                                    <td><asp:Label ID="ItemCategory" runat="server" Text=""></asp:Label></td>
                                    <th>SubCategory</th>
                                    <td><asp:Label ID="Cat02" runat="server" Text=""></asp:Label></td>
                               </tr>
                                 <tr>
                                    <th>Type</th>
                                    <td><asp:Label ID="Cat03" runat="server" Text=""></asp:Label></td>
                                    <th>Cat04</th>
                                    <td><asp:Label ID="Cat04" runat="server" Text=""></asp:Label></td>
                                    <th>Cat05</th>
                                    <td><asp:Label ID="Cat05" runat="server" Text=""></asp:Label></td>
                               </tr>
                                <tr>
                                    <th>RackBarcode</th>
                                    <td><asp:Label ID="RackBarcode" runat="server" Text=""></asp:Label></td>
                                    <th>RackDate</th>
                                    <td><asp:Label ID="RackDate" runat="server" Text=""></asp:Label></td>
                                    <th>printed</th>
                                    <td><asp:Label ID="printed" runat="server" Text=""></asp:Label></td>
                               </tr>
                                <tr>
                                    <th>Colour</th>
                                    <td><asp:Label ID="Colour" runat="server" Text=""></asp:Label></td>
                                    <th>FSN_No</th>
                                    <td><asp:Label ID="FSN_No" runat="server" Text=""></asp:Label></td>
                                    <th>Article_No</th>
                                    <td><asp:Label ID="Article_No" runat="server" Text=""></asp:Label></td>
                               </tr>

                                <tr>
                                    <th>Search_Image_URL</th>
                                    <td><asp:Label ID="Search_Image_URL" runat="server" Text=""></asp:Label></td>
                                    <th>EAN</th>
                                    <td><asp:Label ID="EAN" runat="server" Text=""></asp:Label></td>
                                    <th>Model_Name</th>
                                    <td><asp:Label ID="Model_Name" runat="server" Text=""></asp:Label></td>
                               </tr>

                                <tr>
                                    <th>Description</th>
                                    <td><asp:Label ID="Description" runat="server" Text=""></asp:Label></td>
                                    <th>Comments</th>
                                    <td><asp:Label ID="Comments" runat="server" Text=""></asp:Label></td>
                                    <th>Colour1</th>
                                    <td><asp:Label ID="Colour1" runat="server" Text=""></asp:Label></td>
                               </tr>

                                <tr>
                                    <th>Gender</th>
                                    <td><asp:Label ID="Gender" runat="server" Text=""></asp:Label></td>
                                    <th>Upper_Material</th>
                                    <td><asp:Label ID="Upper_Material" runat="server" Text=""></asp:Label></td>
                                    <th>Sole_Material</th>
                                    <td><asp:Label ID="Sole_Material" runat="server" Text=""></asp:Label></td>
                               </tr>

                                <tr>
                                    <th>Closure</th>
                                    <td><asp:Label ID="Closure" runat="server" Text=""></asp:Label></td>
                                    <th>Occasion</th>
                                    <td><asp:Label ID="Occasion" runat="server" Text=""></asp:Label></td>
                                    <th>Pattern</th>
                                    <td><asp:Label ID="Pattern" runat="server" Text=""></asp:Label></td>
                               </tr>

                                <tr>
                                    <th>Ankle_Height</th>
                                    <td><asp:Label ID="Ankle_Height" runat="server" Text=""></asp:Label></td>
                                    <th>Toe_Shape</th>
                                    <td><asp:Label ID="Toe_Shape" runat="server" Text=""></asp:Label></td>
                                    <th>Heel_Shape</th>
                                    <td><asp:Label ID="Heel_Shape" runat="server" Text=""></asp:Label></td>
                               </tr>

                                <tr>
                                    <th>Heel_Height</th>
                                    <td><asp:Label ID="Heel_Height" runat="server" Text=""></asp:Label></td>
                                    <th>Back_Closure</th>
                                    <td><asp:Label ID="Back_Closure" runat="server" Text=""></asp:Label></td>
                                    <th>Image1</th>
                                    <td><asp:Label ID="image1" runat="server" Text=""></asp:Label></td>
                               </tr>
                                <tr>
                                    <th>Image2</th>
                                    <td><asp:Label ID="image2" runat="server" Text=""></asp:Label></td>
                                    <th>Image3</th>
                                    <td><asp:Label ID="image3" runat="server" Text=""></asp:Label></td>
                                    <th>Image4</th>
                                    <td><asp:Label ID="image4" runat="server" Text=""></asp:Label></td>
                               </tr>
                                <tr>
                                    <th>Image5</th>
                                    <td><asp:Label ID="image5" runat="server" Text=""></asp:Label></td>
                                    <th>Image6</th>
                                    <td><asp:Label ID="image6" runat="server" Text=""></asp:Label></td>
                                    <th>Barcode_Maker</th>
                                    <td><asp:Label ID="Barcode_Maker" runat="server" Text=""></asp:Label></td>
                               </tr>      
                                <tr>
                                    <th>Physical_Location</th>
                                    <td><asp:Label ID="Physical_Location" runat="server" Text=""></asp:Label></td>
                                    <th>Stock Age</th>
                                    <td><asp:Label ID="stockAge" runat="server" Text=""></asp:Label></td>
                                    <th>Lot Age</th>
                                    <td><asp:Label ID="lotAge" runat="server" Text=""></asp:Label></td>
                               </tr>
                                   <tr>
                                       <th>Listing Details</th>
                                       <td ><asp:Label ID="listDets" runat="server" Text=""></asp:Label></td>
                                       <th>EAN</th>
                                       <td ><asp:Label ID="lblEAN" runat="server" Text=""></asp:Label></td>
                                       <th>Initial Status</th><td><asp:Label runat="server" ID="initialStatus"></asp:Label></td>
                                   </tr>
                                    
                                        </table>

                            <table class="table table-bordered table-striped table-hover">
                               
                                <tr>
                                    <th>SalesDate</th>
                                    <th>SalesID</th>
                                    <th>SalesLoc</th>
                                    <th>SalesABWNo</th>
                                    <th>SalesCourier</th>
                                    <th>SoldBy</th>
                                    <th>DispatchDate</th>
                                    <th>DispatchedBy</th>
                                    <th>ReturnDate</th>
                                    <th>ReturnABWNo</th>
                                    <th>ReturnCourier</th>                                   
                                    <th>ReturnReason</th>
                                    <th>ReturnRemarks</th>
                                    <th>ReturnedBy</th>
                                    <th>ReturnedStatus</th>
                                    <th>CancelDate</th>
                                    <th>CancelUser</th>
                                    <th>CancelReason</th>
                                    <th>CancelRemarks</th>
                                </tr>
                                <asp:Repeater runat="server" ID="sales_rpt">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("salesDateTime").ToString() %></td>
                                            <td><%# Eval("salesidgivenbyvloc").ToString() %></td>
                                            <td><%# Eval("vlocs").ToString() %></td>
                                            <td><%# Eval("salesAbwno").ToString() %></td>
                                            <td><%# Eval("courierName").ToString() %></td>
                                            <td><%# Eval("salesUser").ToString() %></td>
                                            <td><%# Eval("dispatchtimestamp").ToString() %></td>
                                            <td><%# Eval("dispatchUser").ToString() %></td>
                                            <td><%# Eval("returntimestamp").ToString() %></td>
                                            <td><%# Eval("returnAbwno").ToString() %></td>
                                            <td><%# Eval("rcourierName").ToString() %></td>                                            
                                            <td><%# Eval("reasons").ToString() %></td>
                                            <td><%# Eval("remarks").ToString() %></td>
                                            <td><%# Eval("retUser").ToString() %></td>
                                            <td><%# Eval("changeStatus").ToString() %></td>
                                            <td><%# Eval("cancelTimeStamp").ToString() %></td>
                                            <td><%# Eval("cancleUser").ToString() %></td>
                                            <td><%# Eval("canclereason").ToString() %></td>
                                            <td><%# Eval("canReason").ToString() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                       
                        </div>
                        </div>
                    </div>
                </div>
                                               </div>
                                        
                                    </section>
         </ContentTemplate>


             
               </asp:UpdatePanel>
                                    
        </div>
</asp:Content>