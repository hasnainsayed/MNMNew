<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="skuInventory.aspx.cs" Inherits="skuInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>SKU Inventory
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">SKU Inventory</li>
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
                        <h3 class="box-title">SKU Inventory</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                        <div runat="server" id="showData" class=" table-responsive">
                            <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>SKU</th>
                                    <th>Title</th>
                                    <th>Category</th>
                                    <th>Brand</th>
                                    <th>MRP</th>
                                    <th>RFL</th>
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("sku").ToString() %></td>
                                           <td><%# Eval("Title").ToString() %></td>
                                           <td><%# Eval("ItemCategory").ToString() %></td>
                                           <td><%# Eval("C1Name").ToString() %></td>
                                           <td><%# Eval("mrp").ToString() %></td>
                                           <td><%# Eval("qnty").ToString() %></td>
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
                                    </ContentTemplate>
               </asp:UpdatePanel>
        </div>
</asp:Content>

