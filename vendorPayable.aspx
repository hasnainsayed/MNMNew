<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vendorPayable.aspx.cs" Inherits="vendorPayable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Payables
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Reports</li>
                <li class="active">Payables</li>
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
                                    <h3 class="box-title">Payables</h3>

                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">

                                    <div runat="server" id="showData" class=" table-responsive">
                                        <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th>Select Vendor</th>
                                                <th></th><th></th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="vendorId" runat="server" CssClass="form-control select2" DataTextField="VendorName" DataValueField="VendorID"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="getVendorPayable" runat="server" OnClick="getVendorPayable_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Payable Report</asp:LinkButton></td>
                                            <td>
                                                    <asp:LinkButton ID="getSuspenseReport" runat="server" OnClick="getSuspenseReport_Click" CssClass="btn btn-sm btn-info"><i class="fa fa-search"></i> Suspense Amount Report</asp:LinkButton></td>
                                            </tr>
                                        </table>


                                        <table class="table table-bordered table-stripped">
                                            <tr>
                                                <th>Total</th>
                                                <th>
                                                    <asp:Label runat="server" ID="checkColumn4Th"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="checkColumn3Th"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="checkColumn2Th"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="checkColumn1Th"></asp:Label></th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="total"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="checkColumn4Sum"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="checkColumn3Sum"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="checkColumn2Sum"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="checkColumn1Sum"></asp:Label></td>
                                            </tr>
                                        </table>
                                        <table class="table table-bordered table-stripped dtSearch">
                                            <thead>
                                                <tr>
                                                    <th>Lot</th>
                                                    <th>Invoice Date</th>
                                                    <th>Invoice No</th>
                                                    <th>Crossed</th>
                                                    <th>Total Amount</th>
                                                    <th>Paid Amount</th>
                                                    <th>Pending Amount</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rtp_List" runat="server" OnItemDataBound="rtp_List_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label Visible="false" runat="server" ID="BagID" Text='<%# Eval("BagID").ToString() %>'></asp:Label>
                                                                <%# Eval("BagDescription").ToString() %>
                                                            </td>
                                                            <td><%# Convert.ToDateTime(Eval("invoiceDate")).ToString("dd MMM yyyy") %></td>
                                                            <td><%# Eval("invoiceNo").ToString() %></td>
                                                            <td><%# Eval("dayDifference").ToString() %></td>
                                                            <td><%# Eval("totalAmount").ToString() %></td>
                                                            <td><%# Eval("totalPaidAmount").ToString() %>
                                                                <br />
                                                                <div id='h<%# Container.ItemIndex%>' style="cursor:pointer;color:firebrick;" onclick="getdata(<%# Container.ItemIndex%>)">Payment Details</div>

                                                                <div id='d<%# Container.ItemIndex%>' style="visibility:hidden;display:none;">                                              <table class="table table-bordered table-stripped">

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

                                                                        </table></div>
            

                                                            </td>
                                                            <td><%# Eval("pendingAmount").ToString() %></td>
                                                            <td><%# Eval("paymentStatus").ToString() %></td>

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
    <script>
        function getdata(id) {
            var e = document.getElementById('d' + id);
            if (e) {
                if (e.style.display != 'block') {
                    e.style.display = 'block';
                    e.style.visibility = 'visible';
                } else {
                    e.style.display = 'none';
                    e.style.visibility = 'hidden';
                }
            }
        }
    </script>
</asp:Content>

