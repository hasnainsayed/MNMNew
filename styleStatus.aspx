<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="styleStatus.aspx.cs" Inherits="styleStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %><%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Style History
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Style History</li>
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
                        <h3 class="box-title">Style History</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <asp:Panel ID="Panel3" runat="server" DefaultButton="styleHistory">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                           <table class="table table-bordered">
                               <tr>
                                     <td>
                                         <asp:TextBox ID="styleCode" runat="server" CssClass="form-control"></asp:TextBox>
                                     </td>
                                   <td>
                                       <asp:LinkButton ID="styleHistory" runat="server" CssClass="btn btn-sm btn-warning" OnClick="styleHistory_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                   </td>
                                  
                               </tr>
                       
                           </table>
                                    </ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>
                            </div>
                        <div runat="server" id="showData" visible="false" class=" table-responsive">
                            <table class="table table-bordered table-stripped">
                                <thead>
                                    <tr>
                                        <th colspan="3">Style Details</th>
                                    </tr>
                                <tr>
                                    
                                    <th>User</th>
                                    <th>Action</th>
                                    <th>DateTime</th>
                                    
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rpt_Style" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Details").ToString() %></td>
                                           <td><%# Eval("Dets").ToString() %></td>
                                           
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>

                            <table class="table table-bordered table-stripped dtSearchDesc">
                                <thead>
                                   
                                <tr>
                                    
                                    <th>Barcode</th>
                                    <th>Status</th>
                                    <th>DateTime</th>
                                    <th>View Status</th>
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rpt_barcode" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           
                                           <td>
                                               <asp:Label ID="lblBarcodeNo" runat="server" Visible="false" Text='<%# Eval("BarcodeNo").ToString() %>'></asp:Label><%# Eval("BarcodeNo").ToString() %></td>
                                           <td><%# Eval("Status").ToString() %></td>
                                           <td><%# Eval("SystemDate").ToString() %></td>
                                           <td>
                                               <asp:LinkButton ID="shoeBarcodeStatus" runat="server" CssClass="btn btn-dropbox btn-sm" OnClick="shoeBarcodeStatus_Click"><i class="fa fa-search"></i> Barcode History</asp:LinkButton></td>
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>


                            <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                                <th>User</th>
                                                                <th>SKU</th>
                                                                <th>List ID</th>
                                                                <th>Location</th>
                                                                <th>Type</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="Listedrpt" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("username").ToString() %></td>
                                                                        <td><%# Eval("sku").ToString() %></td>
                                                                        <td><%# Eval("listID").ToString() %></td>
                                                                        <td><%# Eval("Location").ToString() %></td>
                                                                        <td><%# Eval("type").ToString() %></td>
                                                                        <td><%# Eval("datetime").ToString() %></td>
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
                                        
                                    </section>



                                                 <asp:Panel ID="PanelSinglePopUp" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button99" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender299"
                    runat="server"
                    TargetControlID="Button99"
                    PopupControlID="PanelSinglePopUp"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe99">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog" style="overflow-y:scroll;height:500px;">
                    <div class="modal-content cap-mod">
                        <div class="modal-header">
                           
                            <h4 class="modal-title">BARCODE : <asp:Label runat="server" ID="lblBarcodeMaster"></asp:Label>
                               
                            </h4>
                        </div>
                        <div class="modal-body table-responsive">
                   <div class="col-md-12">
        <table class="table table-bordered table-stripped">
                                <thead>
                                <tr>
                                    
                                    <th>User</th>
                                    <th>Style Action</th>
                                    <th>DateTime</th>
                                    
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rpt_StyleMaster" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Details").ToString() %></td>
                                           <td><%# Eval("Dets").ToString() %></td>
                                           
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>

                       <table class="table table-bordered table-stripped dtSearchDesc">
                                <thead>
                                <tr>
                                    <th>User</th>
                                    <th>Barcode Status</th>
                                    <th>DateTime</th>
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_ListMaster" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Status").ToString() %></td>
                                           <td><%# Convert.ToDateTime(Eval("DateTime")).ToString("dd MMM yyyy HH:mm:ss tt") %></td>
                                           
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>
					</div>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button333" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup99()" />
                  
        
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>
                                    </ContentTemplate>


             
               </asp:UpdatePanel>
        </div>
</asp:Content>

