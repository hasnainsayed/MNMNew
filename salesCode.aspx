<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="salesCode.aspx.cs" Inherits="salesCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        
        <!-- take from here -->
        <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Sales</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                            <div class="col-md-6">
                                <table class="table table-bordered table-striped">
                                 <tr>
                                    <th>Virtual location</th>
                                    <td>
                                        <asp:Label ID="lblStyleID" runat="server" Text="" visible="false"></asp:Label>
                                     <asp:Label ID="lblGSTPercent" runat="server" Text="" visible="false"></asp:Label>
                                        <asp:DropDownList ID="virtualLocation" runat="server" DataTextField="Location" DataValueField="locationID" AutoPostBack="true" OnSelectedIndexChanged="virtualLocation_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <th>SalesID</th>
                                    <td><asp:TextBox ID="salesId" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <th>Customer Name</th>
                                    <td><asp:TextBox ID="custname" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <th>Address 1</th>
                                    <td><asp:TextBox ID="address1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <th>Address 2</th>
                                    <td><asp:TextBox ID="address2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <th>City</th>
                                    <td><asp:TextBox ID="city" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <th>State</th>
                                    <td>
                                        <asp:DropDownList ID="stateID" runat="server" DataTextField="statename" DataValueField="stateid" CssClass="form-control select2"></asp:DropDownList></td>
                                </tr>
                             </table>
                            </div>
                             
                             
                        <div class="col-md-6">
                        <table class="table table-bordered table-striped" >
                                <tr>
                                    <th>Select</th>
                                    <th>Barcode</th>
                                    <th>Location</th>
                                    <th>SP</th>
                                </tr>
                                <asp:Repeater ID="rptSales" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <asp:Label ID="stockUpId" runat="server" Visible="false" Text='<%# Eval("StockUpID").ToString() %>'></asp:Label>
                                               <asp:CheckBox ID="sales" runat="server" /></td>
                                           <td><%# Eval("fullBarcode").ToString() %></td>
                                           <td><%# Eval("RackBarcode").ToString() %></td>
                                           <td><asp:TextBox ID="sp" runat="server" CssClass="form-control" Text='<%# Eval("listprice").ToString() %>'></asp:TextBox></td>
                                       </tr>
                                   </ItemTemplate>
                                </asp:Repeater>
                                
                            </table>
                            </div>
                            <div class="box-body table-responsive ">
                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                <asp:Button ID="saveSales"  class="btn btn-primary pull-right btn-round" runat="server" Text="Generate Invoice" OnClick="saveSales_Click"/>
                               
                            </div>
                        </div>
                            </div></div></div></div>

</asp:Content>

