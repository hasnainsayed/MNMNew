<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="invoicePayment.aspx.cs" Inherits="invoicePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Make Invoice Payment
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="invoice.aspx">Invoice</a></li>
                <li class="active">Make Invoice Payment</li>
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
                                <tr>
                                    <th colspan="2">Invoice Details</th>
                                </tr>
                                <tr>
                                    <th>Customer</th>
                                    <td>
                                        <asp:Label runat="server" ID="custname"></asp:Label>
                                        <asp:Label runat="server" ID="webCustomer" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Invoice No.</th>
                                    <td>
                                        <asp:Label runat="server" ID="invid"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Invoice Date</th>
                                    <td>
                                        <asp:Label runat="server" ID="salesDate"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Amount</th>
                                    <td>
                                        <asp:Label runat="server" ID="total"></asp:Label></td>
                                </tr>
                            </table>

                            <table class="table table-bordered table-stripped">
                                <tr>
                                    <th colspan="5">Payment Details</th>
                                </tr>
                                <tr>
                                    <th>Date</th>
                                    <th>Amount</th>
                                    <th>Centre</th>
                                    <th>Payment Mode</th>
                                    <th>Transfer/Cheque No.</th>
                                    <th>Remarks</th>
                                </tr>
                                <asp:Repeater ID="paymentRpt" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Convert.ToDateTime(Eval("paymentDate")).ToString("dd MMM yyyy")%></td>
                                            <td><%# Eval("paymentAmount")%></td>
                                            <td><%# Eval("centreName")%></td>
                                            <td><%# Eval("modeName")%></td>
                                            <td><%# Eval("paymentTransaction")%></td>
                                            <td><%# Eval("paymentRemarks")%></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <th>Paid Amount</th>
                                    <td>
                                        <asp:Label runat="server" ID="paidAmountTD"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Pending Amount</th>
                                    <td>
                                        <asp:Label runat="server" ID="pendingAmount"></asp:Label></td>
                                </tr>
                            </table>
                            <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Customer</label>
                                                <asp:DropDownList runat="server" ID="customerId" CssClass="form-control select2" DataTextField="custFirstName" DataValueField="webCustId"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Date</label>
                                                <asp:TextBox runat="server" ID="paymentDate" CssClass="form-control datepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Remarks</label>
                                                <asp:TextBox runat="server" ID="paymentRemarks" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                            <div class="box-body table-responsive">
                                     <div class="col-md-12 pull-right">
                                        <asp:LinkButton runat="server" ID="addMoreMoney" CssClass="btn btn-sm btn-primary" OnClick="addMoreMoney_Click"><i class="fa fa-plus"></i> Add</asp:LinkButton>
                                    </div>
                                    <div class="col-md-12" style="color:darkred;font-size:20px;">Calculated Amount : 
                                        <asp:Label runat="server" ID="calculatedAmount"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                    <asp:Repeater ID="rptPayments" runat="server" OnItemDataBound="rptPayments_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="box-header with-border">
                                                    <h3 class="box-title">Sr No. #<%# Eval("moneyNo").ToString() %></h3>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Centre</label>
                                                        <asp:DropDownList runat="server" ID="paymentCentre" CssClass="form-control select2" DataTextField="centreName" DataValueField="centreId"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Payment Mode</label>
                                                        <asp:DropDownList runat="server" ID="paymentMode" CssClass="form-control select2" selectedValue='<%# Eval("paymentMode").ToString() %>'>
                                                            <asp:ListItem Value="1">Cash</asp:ListItem>
                                                            <asp:ListItem Value="2">Transfer</asp:ListItem>
                                                            <asp:ListItem Value="3">Cheque</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Amount</label>
                                                        <asp:TextBox runat="server" ID="paymentAmount" CssClass="form-control" Text='<%# Eval("paymentAmount").ToString() %>'></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Transaction Details</label>
                                                        <asp:TextBox runat="server" ID="paymentTransaction" CssClass="form-control" Text='<%# Eval("paymentTransaction").ToString() %>'></asp:TextBox>
                                                    </div>
                                                </div>


                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div> </div>
                                <!-- /.box-body -->

                                <div class="box-body table-responsive">
                                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                        <asp:Button ID="btnSave" class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return confirm('Do you Want to Save ?');" />
                                        <asp:Button ID="btnCancel" class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                </div>
                        </div>
                        <!-- /.box-body -->
                    </div>

                    <!-- /.box -->
                </div>
              
        </section>
    </div>
</asp:Content>

