<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="suspenseReceivable.aspx.cs" Inherits="suspenseReceivable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content-header">
            <h1>Suspense Receivable
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Reports</li>
                <li class="active">Suspense Receivable</li>
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
                                    <h3 class="box-title">Suspense Receivable - <asp:Label runat="server" ID="custName"></asp:Label>
                                        <asp:Label runat="server" ID="custId" Visible="false"></asp:Label>
                                    </h3>

                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">

                                    <div runat="server" id="showData" class=" table-responsive">
                                        <table class="table table-bordered table-stripped ">
                                            <thead>
                                                <tr>       
                                                    <th>Invoice No</th>
                                                    <th>Date</th>
                                                    <th>Centre</th>
                                                    <th>Mode</th>
                                                    <th>Transaction Details</th>
                                                    <th>Remarks</th>
                                                    <th>Total Amount</th>                                                   
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rtp_List" runat="server">
                                                    <ItemTemplate>
                                                        <tr>      
                                                            <td><asp:Label runat="server" ID="invoiceNo" Text='<%# Eval("invoiceId").ToString() %>'></asp:Label></td>
                                                            <td><%# Convert.ToDateTime(Eval("paymentDate")).ToString("dd MMM yyyy") %></td>                                                            
                                                            <td><%# Eval("centreName").ToString() %></td>  
                                                            <td><%# Eval("modeName").ToString() %></td>  
                                                            <td><%# Eval("paymentTransaction").ToString() %></td>  
                                                            <td><%# Eval("paymentRemarks").ToString() %></td> 
                                                            <td><%# Eval("paymentAmount").ToString() %></td> 
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <div class="pull-right footer">
                                            <asp:LinkButton runat="server" ID="backToReceivable" OnClick="backToReceivable_Click" CssClass="btn btn-sm btn-primary">Back</asp:LinkButton>
                                        </div>
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

