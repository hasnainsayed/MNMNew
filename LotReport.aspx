<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LotReport.aspx.cs" Inherits="LotReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="content-wrapper"> <section class="content-header">
        <h1>Lot Report 
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Report One</li>
            <li class="active">Lot Report </li>
        </ol>
    </section>

     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                  <section class="content">
      <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary" id="printPanel">
                    <div class="box-header with-border">
                        <h3 class="box-title"><asp:LinkButton ID="btnexporttoexcel" runat="server" CssClass="btn btn-success pull-right btn-round" OnClick="btnexporttoexcel_Click"><i class="fa  fa-file-excel-o"></i> Export</asp:LinkButton></h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div runat="server" id="pickListTable">
                        <div class="row">
                           <table class="table table-bordered table-striped table-hover dtSearch">
                                <thead>
                                            <tr>
                                                <th>Lot</th>
                                                 <th>Lot Qty</th>
                                                 <th>Lot Amount</th>
                                               <%--  <th>Barcode</th>--%>
                                                 <th>Sold Qty</th>
                                                 <th>Available Qty</th>
                                                 <th>Amount Earned</th>
                                                
                                                
                                            </tr>
                                    </thead>
                                <tbody>
                                            <asp:Repeater ID="rpt_Lot" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("Lot#").ToString() %></td>
                                                            <td> <%# Eval("Lot_Qty").ToString() %></td>
                                                           <td>  <%# Eval("Lot_Amnt").ToString() %></td>
                                                         <%--  <td> <%# Eval("Barcode").ToString() %></td>--%>
                                                           <td> <%# Eval("Sold_Qty").ToString() %></td>
                                                             <td><%# Eval("Avail_Qty").ToString() %></td>
                                                            <td> <%# Eval("Amnt_Earned").ToString() %></td>
                                                            
                                                             
                                                             

                                                       
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
                            </div>                   
                       </section>

                </ContentTemplate>
          <Triggers>
                                            <asp:PostBackTrigger ControlID="btnexporttoexcel" />
                                            </Triggers>
         </asp:UpdatePanel>
         </div>
</asp:Content>

