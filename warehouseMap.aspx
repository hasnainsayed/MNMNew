<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="warehouseMap.aspx.cs" Inherits="warehouseMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Warehouse Map
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Warehouse Map</li>
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
                        <h3 class="box-title">Warehouse Map</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                        <div runat="server" id="showData" class=" table-responsive">
                            <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>Rack</th>
                                    <th>Limit</th>
                                    <th>Occupied</th>
                                    <th>Available</th>
                                    
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("RackNo").ToString() %></td>
                                           <td><%# Eval("RackLimitation").ToString() %></td>
                                           <td><%# Eval("racked").ToString() %></td>
                                           <td><%# Eval("spaceLeft").ToString() %></td>
                                           
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

