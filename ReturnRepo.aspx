<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReturnRepo.aspx.cs" Inherits="ReturnRepo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> <section class="content-header">
        <h1>Return Report 
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Report </li>
            <li class="active">Return Report </li>
        </ol>
    </section>

    <%-- <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>--%>
                  <section class="content">
      <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary" id="printPanel">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                         <table class="table table-bordered">
                               <tr>
                                   <td style="font-weight:bold;font-size:large" >
                                       Search By
                                   </td>
                                   
                                    <td>
                                    <asp:RadioButtonList ID="rbttype" class="form-control" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Size="Large" Font-Bold="True" >

                                        <asp:ListItem Text="Sort By Month" Value="Month"></asp:ListItem>

                                        <asp:ListItem Text="Sort By Date" Value="Date"  style="margin-left: 15px;"></asp:ListItem>
                                     
                                    </asp:RadioButtonList>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
    ControlToValidate="rbttype" Text="Required" Font-Bold="True" ForeColor="red" 
    ValidationGroup="go">
</asp:RequiredFieldValidator>
                           
                                   </td>
                                   <td  width="7%";>
                                       <asp:LinkButton ID="btnsearch" runat="server" CssClass="btn btn-primary pull-left" OnClick="btnsearch_Click" ValidationGroup="go"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                       </td>
                                   <td>
                                       <asp:LinkButton ID="btnexporttoexcel" runat="server" CssClass="btn btn-success pull-left " OnClick="btnexporttoexcel_Click" ValidationGroup="go"><i class="fa  fa-file-excel-o"></i> Export</asp:LinkButton>
                                   </td>
                                   
                                  
                                   
                               </tr>
                               
                           </table>
                       <div runat="server" id="sortdiv" visible="false">
                           <table class="table table-bordered table-striped table-hover dtSearch">
                                <thead>
                                            <tr>
                                               
                                                 <th runat="server" id="thdate" visible="false">DD-MM-YY</th>
                                                 <th>Month</th>
                                                 <th>Year</th>
                                                 <th>Channel</th>
                                                 <th>Total Order Prossed</th>
                                                 <th>Orders Rerurned</th>
                                                 <th>Amount Charged</th>
                                                
                                                
                                            </tr>
                                    </thead>
                                <tbody>
                                            <asp:Repeater ID="rpt_Return" runat="server" OnItemDataBound="rpt_Return_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                       
                                                           <td id="tddate" runat="server"  visible="false"> <%# Eval("Date")%></td>
                                                           <td>  <%# Eval("Month").ToString() %></td>
                                                           <td> <%# Eval("year").ToString() %></td>
                                                           <td><%# Eval("Channel").ToString() %></td>
                                                           <td><%# Eval("TotalOrder").ToString() %></td>
                                                           <td> <%# Eval("ReturnOrder").ToString() %></td>
                                                           <td> <%# Eval("Amount Charged").ToString() %></td>
                                                           
                                                             
                                                             

                                                       
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </tbody>
                                <tfoot></tfoot>
                                        </table>
                           </div>
                             </div>
                             </div>
                        </div>
                    </div>
                                 
                       </section>

                <%--</ContentTemplate>
         </asp:UpdatePanel>--%>
         </div>
</asp:Content>

