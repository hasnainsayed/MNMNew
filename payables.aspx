<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="payables.aspx.cs" Inherits="payables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="content-wrapper"> <section class="content-header">
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
                            <table class="table table-bordered table-stripped">
                                <tr>
                                    <th>Total</th>
                                    <th><asp:Label runat="server" ID="checkColumn4Th"></asp:Label></th>
                                    <th><asp:Label runat="server" ID="checkColumn3Th"></asp:Label></th>
                                    <th><asp:Label runat="server" ID="checkColumn2Th"></asp:Label></th>
                                    <th><asp:Label runat="server" ID="checkColumn1Th"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td><asp:Label runat="server" ID="total"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="checkColumn4Sum"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="checkColumn3Sum"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="checkColumn2Sum"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="checkColumn1Sum"></asp:Label></td>
                                </tr>
                            </table>
                            <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>Vendor</th>
                                    <th>Total</th>                                   
                                    <th><asp:Label runat="server" ID="checkColumn4"></asp:Label></th>
                                    <th><asp:Label runat="server" ID="checkColumn3"></asp:Label></th>
                                    <th><asp:Label runat="server" ID="checkColumn2"></asp:Label></th>
                                    <th><asp:Label runat="server" ID="checkColumn1"></asp:Label></th>
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_List" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><asp:Label Visible="false" runat="server" ID="VendorID" Text='<%# Eval("VendorID").ToString() %>'></asp:Label>
                                               <asp:LinkButton runat="server" ID="vendorPayable" OnClick="vendorPayable_Click"><%# Eval("VendorName").ToString() %></asp:LinkButton> 
                                           </td>
                                           <td><%# Eval("totalAmount").ToString() %></td>                                           
                                           <td><%# Eval("checkFourAmount").ToString() %></td>
                                           <td><%# Eval("checkThreeAmount").ToString() %></td>
                                           <td><%# Eval("checkTwoAmount").ToString() %></td>
                                           <td><%# Eval("checkOneAmount").ToString() %></td>
                                           
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

