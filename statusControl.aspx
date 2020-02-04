<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="statusControl.aspx.cs" Inherits="statusControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Status Control
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Status Control</li>
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
                        <h3 class="box-title">Status Control</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                        
                             <asp:Panel ID="Panel3" runat="server" DefaultButton="searchBarcode">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                            <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th>Barcode</th>
                                                <th>Search</th>
                                            </tr>
                                            <tr>
                                               
                                                <td><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td><asp:LinkButton ID="searchBarcode" runat="server" OnClick="searchBarcode_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                            </tr>
                                        </table>
                        </ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>

                               <div runat="server" id="showData" class=" table-responsive">      
                            <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>BarcodeNo</th>
                                    <th>Vertical</th>
                                    <th>Title</th>
                                    <th>Brand</th>
                                    <th>MRP</th>
                                    <th width="15%">Status</th>
                                    <th>Change Status</th>
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server" OnItemDataBound="rtp_List_ItemDataBound">
                                   <ItemTemplate>
                                       <tr>
                                           <td><asp:Label runat="server" ID="StockupID" Visible="false" Text='<%# Eval("StockupID").ToString() %>'></asp:Label>
                                               <asp:Label runat="server" ID="barcode" Visible="false" Text='<%# Eval("BarcodeNo").ToString() %>'></asp:Label>
                                               <%# Eval("BarcodeNo").ToString() %>
                                           </td>
                                           <td><%# Eval("ItemCategory").ToString() %></td>
                                           <td><%# Eval("Title").ToString() %></td>
                                           <td><%# Eval("C1Name").ToString() %></td>
                                           <td><%# Eval("mrp").ToString() %></td>
                                           <td><asp:Label runat="server" ID="currentStatus" Visible="false" Text='<%# Eval("Status").ToString() %>'></asp:Label>
                                               <asp:DropDownList ID="Status" runat="server" CssClass="form-control select2">
                                                   <asp:ListItem Value="RFL">RFL</asp:ListItem>
                                                   <asp:ListItem Value="NRM">Minor</asp:ListItem>
                                                   <asp:ListItem Value="NRD">Damaged</asp:ListItem>
                                                   <asp:ListItem Value="NRR">Retail</asp:ListItem>
                                               </asp:DropDownList>

                                           </td>
                                           <td>
                                               <asp:LinkButton ID="changeStatus" runat="server" CssClass="btn btn-sm btn-reddit" OnClick="changeStatus_Click" OnClientClick="return confirm('Do you Want to Change Status ?');">Change Status</asp:LinkButton></td>
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
                                         </ContentTemplate>


             
               </asp:UpdatePanel>
                                    </section>
                                    
        </div>
</asp:Content>

