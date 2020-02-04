<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="lotPayment.aspx.cs" Inherits="lotPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content-header">
            <h1>Make Lot Payment
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="newLot.aspx">Lot</a></li>
                <li class="active">Make Lot Payment</li>
            </ol>
        </section>
         <section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            <asp:Label ID="lotId" runat="server" Text="" Visible="false"></asp:Label> 
                
                <div class="box">
                   
                    <div class="box-body table-responsive">
                        <table class="table table-bordered table-stripped">
                            <tr><th colspan="2">Lot Details</th></tr>
                            <tr>
                                <th>Vendor</th>
                                <td><asp:Label runat="server" ID="vendorName"></asp:Label>
                                    <asp:Label runat="server" ID="vendorId" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>LOT</th>
                                <td><asp:Label runat="server" ID="lotNo"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Invoice Date</th>
                                <td><asp:Label runat="server" ID="invoiceDate"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Invoice No.</th>
                                <td><asp:Label runat="server" ID="invoiceNo"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Total Pieces</th>
                                <td><asp:Label runat="server" ID="lotPiece"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Amount</th>
                                <td><asp:Label runat="server" ID="lotAmount"></asp:Label></td>
                            </tr>
                        </table>

                        <table class="table table-bordered table-stripped">
                            <tr><th colspan="5">Payment Details</th></tr>
                            <tr><th>Date</th><th>Amount</th><th>Centre</th><th>Payment Mode</th><th>Transfer/Cheque No.</th><th>Remarks</th></tr>
                        <asp:Repeater ID="paymentRpt" runat="server" >
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Convert.ToDateTime(Eval("paymentDate")).ToString("dd MMM yyyy")%></td>
                                            <td><%# Eval("paymentAmount")%></td>
                                            <td><%# Eval("centreName")%></td>
                                            <td><%# Eval("modeName")%></td>
                                            <td><%# Eval("paymentTransaction")%></td>
                                            <td><%# Eval("paymentRemarks")%></td>
                                            
                                        </tr>
                                        </ItemTemplate></asp:Repeater>
                        <tr><th>Paid Amount</th><td><asp:Label runat="server" ID="paidAmountTD"></asp:Label></td></tr>
                        <tr><th>Pending Amount</th><td><asp:Label runat="server" ID="pendingAmount"></asp:Label></td></tr>
                        </table>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Centre</label>
                                    <asp:DropDownList runat="server" ID="paymentCentre" CssClass="form-control select2" DataTextField="centreName" DataValueField="centreId"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Payment Mode</label>
                                    <asp:DropDownList runat="server" ID="paymentMode" CssClass="form-control select2">
                                        <asp:ListItem Value="1">Cash</asp:ListItem>
                                        <asp:ListItem Value="2">Transfer</asp:ListItem>
                                        <asp:ListItem Value="3">Cheque</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Amount</label>
                                    <asp:TextBox runat="server" ID="paymentAmount" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Transaction Details</label>
                                    <asp:TextBox runat="server" ID="paymentTransaction" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Date</label>
                                    <asp:TextBox runat="server" ID="paymentDate" CssClass="form-control datepicker"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Remarks</label>
                                    <asp:TextBox runat="server" ID="paymentRemarks" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" OnClientClick="return confirm('Do you Want to Save ?');"/>
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
         </div>
</asp:Content>

