<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rolemaster.aspx.cs" Inherits="rolemaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content-header">
            <h1>Roles  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="rolemaster.aspx">Roles</a></li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                 <div class="box box-primary" id="devCapone" runat="server" visible="false">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                <div id="divErr" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saving Failed</h5>

                                    </div>
                         <div class="row">

                             <div class="col-md-12">
                                <div class="form-group">
                                    <label>Role </label>
                                    <asp:TextBox ID="roleName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-12">
                                     <table class="table table-bordered table-striped table-hover">
                                         <tr><th colspan="6">MASTERS</th></tr>
                                         <tr>
                                             <td><asp:CheckBox ID="LocationType" runat="server" /> Location Type</td>
                                             <td><asp:CheckBox ID="PhysicalLocation" runat="server" /> Physical Location</td>
                                             <td><asp:CheckBox ID="VirtualLocation" runat="server" /> Virtual Location</td>
                                             <td><asp:CheckBox ID="Vendors" runat="server" /> Vendors</td>
                                             <td><asp:CheckBox ID="newLot" runat="server" /> LOT</td>
                                             <td><asp:CheckBox ID="hsnCode" runat="server" /> HSN</td>
                                         </tr>
                                         <tr>
                                             <td><asp:CheckBox ID="courier" runat="server" /> Courier</td>
                                             <td><asp:CheckBox ID="usermaster" runat="server" /> Users</td>
                                              <td><asp:CheckBox ID="rolemasters" runat="server" /> Roles</td><td></td><td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">SETTINGS</th></tr>
                                         <tr>
                                             
                                             <td><asp:CheckBox ID="ColumnTableSetting" runat="server" /> DropDown Value</td>
                                             <td><asp:CheckBox ID="StyleColumnSetting" runat="server" /> Data Field Value</td>
                                             <td></td><td></td><td></td><td></td>
                                             
                                         </tr>
                                         <tr><th colspan="6">STYLE CATEGORY</th></tr>
                                         <tr>
                                             <td><asp:CheckBox ID="ItemCategory" runat="server" /> Vertical</td>
                                             <td><asp:CheckBox ID="Size" runat="server" /> Size</td>
                                             <td><asp:CheckBox ID="Category2" runat="server" /> Sub Category</td>
                                             <td><asp:CheckBox ID="Category3" runat="server" /> Type</td>
                                             <td><asp:CheckBox ID="Category4" runat="server" /> CAT 04</td>
                                             <td><asp:CheckBox ID="Category5" runat="server" /> CAT 05</td>
                                         </tr>
                                         <tr><th colspan="6">STYLE COLUMN</th></tr>
                                         <tr>                                            
                                             <td><asp:CheckBox ID="Column1" runat="server" /> Brand</td>
                                             <td><asp:CheckBox ID="Column2" runat="server" /> Colour</td>
                                             <td><asp:CheckBox ID="Column3" runat="server" /> Gender</td>
                                             <td><asp:CheckBox ID="Column4" runat="server" /> Upper Material</td>
                                             <td><asp:CheckBox ID="Column5" runat="server" /> Sole Material</td>
                                             <td><asp:CheckBox ID="Column6" runat="server" /> Closure</td>
                                         </tr>
                                         <tr>                                            
                                             <td><asp:CheckBox ID="Column7" runat="server" /> Occassion</td>
                                             <td><asp:CheckBox ID="Column8" runat="server" /> Pattern</td>
                                             <td><asp:CheckBox ID="Column9" runat="server" /> Ankle Height</td>
                                             <td><asp:CheckBox ID="Column10" runat="server" /> Toe Shape</td>
                                             <td><asp:CheckBox ID="Column11" runat="server" /> Heel Shape</td>
                                             <td><asp:CheckBox ID="Column12" runat="server" /> Heel Height</td>
                                         </tr>
                                         <tr>                                             
                                             <td><asp:CheckBox ID="Column13" runat="server" /> Back Closure</td>
                                             <td><asp:CheckBox ID="Column14" runat="server" /> Column 14</td>
                                             <td><asp:CheckBox ID="Column15" runat="server" /> Column 15</td>
                                             <td><asp:CheckBox ID="Column16" runat="server" /> Column 16</td>
                                             <td><asp:CheckBox ID="Column17" runat="server" /> Column 17</td>
                                             <td><asp:CheckBox ID="Column18" runat="server" /> Column 18</td>
                                         </tr>
                                         <tr>
                                             <td><asp:CheckBox ID="Column19" runat="server" /> Column 19</td>
                                             <td><asp:CheckBox ID="Column20" runat="server" /> Column 20</td>
                                             <td></td><td></td><td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">Item Style</th></tr>
                                          <tr>
                                             <td><asp:CheckBox ID="ItemStyleSearchAdd" runat="server" /> Item Style</td>
                                             <td><asp:CheckBox ID="addStyle" runat="server" /> Add Style</td>
                                             <td><asp:CheckBox ID="addRFLNR" runat="server" /> Add RFL/NR</td>
                                             <td></td><td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">View Inventory</th></tr>
                                          <tr>
                                             <td><asp:CheckBox ID="ViewInventory" runat="server" /> View Inventory</td>
                                             <td><asp:CheckBox ID="listing" runat="server" /> Listing</td>
                                             <td><asp:CheckBox ID="sellingInd" runat="server" /> Selling </td>
                                             <td></td><td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">General</th></tr>
                                         <tr>                                             
                                             <td><asp:CheckBox ID="invoice" runat="server" /> Invoice</td>
                                             <td><asp:CheckBox ID="returnItem" runat="server" /> Mark Return</td>
                                             <td><asp:CheckBox ID="statusControl" runat="server" /> Status Control</td>
                                             <td><asp:CheckBox ID="cancleBarcode" runat="server" /> Cancel Barcode</td>
                                             <td><asp:CheckBox ID="changeMRP" runat="server" /> Change MRP</td>
                                             <td><asp:CheckBox ID="deleteWrongBarcode" runat="server" /> Delete Barcode</td>
                                         </tr>
                                         <tr><th colspan="6">Ticket</th></tr>
                                         <tr>                                             
                                             <td><asp:CheckBox ID="tickets" runat="server" /> Ticket List</td>
                                             <td><asp:CheckBox ID="addTicket" runat="server" /> Add Ticket</td>
                                            <td></td><td></td><td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">Pick List</th></tr>
                                         <tr>             
                                             <td><asp:CheckBox ID="pickList" runat="server" /> PicK List</td>
                                             <td><asp:CheckBox ID="pickDispatch" runat="server" /> Dispatch</td>
                                             <td><asp:CheckBox ID="pickCancel" runat="server" /> Cancel</td>
                                             <td></td><td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">Reports</th></tr>
                                         <tr>                                             
                                             <td><asp:CheckBox ID="notListed" runat="server" /> SKU Not Listed</td>
                                             <td><asp:CheckBox ID="skuInventory" runat="server" /> SKU Inventory</td>
                                             <td><asp:CheckBox ID="dumps" runat="server" /> Dump</td>
                                             <td><asp:CheckBox ID="salesDump" runat="server" /> Sales Dump</td>
                                             <td><asp:CheckBox ID="dispatchedDump" runat="server" /> Dispatched Dump</td>
                                             <td><asp:CheckBox ID="warehouseMap" runat="server" /> Warehouse Map</td>
                                         </tr>
                                         <tr>                                             
                                             <td><asp:CheckBox ID="userWork" runat="server" /> User Work</td>
                                             <td><asp:CheckBox ID="barcodeStatus" runat="server" /> Barcode Status</td>
                                             <td><asp:CheckBox ID="styleStatus" runat="server" /> Style Status</td>
                                             <td><asp:CheckBox ID="ReportsModule" runat="server" /> Reports Module</td>
                                             <td></td><td></td>
                                         </tr>
                                         <tr><th colspan="6">Bulk</th></tr>
                                         <tr>                                             
                                             <td><asp:CheckBox ID="bulkListing" runat="server" /> Bulk Listing</td>
                                             <td><asp:CheckBox ID="saleScan" runat="server" /> Sales Scan</td>
                                             <td><asp:CheckBox ID="saleExcel" runat="server" /> Sales Excel</td>
                                             <td><asp:CheckBox ID="dispatchScan" runat="server" /> Dispatch Scan</td>
                                             <td><asp:CheckBox ID="dispatchExcel" runat="server" /> Dispatched Excel</td>
                                             <td></td>
                                         </tr>
                                     </table>
                                 </div>

                            </div>

                           
                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="grp"/>
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-sm" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
                    </div>
                </div>
                <!-- /.box -->
            </div>
            </div>
     
        
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label> 
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Button ID="btnadd" runat="server" Text="Add New" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          </div>
                    <div class="box-body table-responsive">
                           <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>

                                    <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saving Failed</h5>

                                    </div>
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <td>ID</td>
                                   <td>Role </td>
                                   
                                   <td>Action</td>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("roleId")%>
                                            <asp:Label ID="roleId" runat="server" Text='<%# Eval("roleId").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                            <td><%# Eval("roleName")%></td> 
                                          
                                            <td>
                                                <asp:LinkButton ID="edit" runat="server" OnClick="edit_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                               
                                            </td>
                                           

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>



                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
                            </asp:UpdatePanel>
    </div>
</asp:Content>

