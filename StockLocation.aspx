<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockLocation.aspx.cs" Inherits="StockLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
    <section class="content-header">
            <h1>Stock Location
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                 <li><a href="#"><i class="fa fa-dashboard"></i>Report</a></li>
                <li class="active"><a  href="StockLocation.aspx">Stock Location  </a></li>
               
            </ol>
        </section>
     
   
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Search Box</h3>
                                <div class="pull-right">
                                </div>
                            </div>
                            <!-- /.box-header -->


                            <div class="box-body">

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="box box-widget widget-user-">
                                            <!-- Add the bg color to the header using any of the bg-* classes -->

                                            <div class="box-header">
                                                <h3 class="box-title">Summary Box
                                                </h3>
                                            </div>


                                            <div class="box-footer no-padding">
                                                <ul class="nav nav-stacked">
                                                    <li><a href="#" style="background: white; cursor: default">Total MRP <span class="pull-right badge bg-blue">
                                                        <asp:Label runat="server" ID="lbl1" Text="0"></asp:Label></a></span></li>
                                                    <%--  <li><a href="#">Tasks <span class="pull-right badge bg-aqua">5</span></a></li>--%>
                                                    <li><a href="#" style="background: white; cursor: default">Total Count <span class="pull-right badge bg-green"> 
                                                        <asp:Label runat="server" ID="lbl2" Text="0"></asp:Label></span></a></li>
                                                    <%--<li><a href="#" style="background: white; cursor:default">Pending Task   <span class="pull-right badge bg-red"><asp:Label runat="server" ID="lblpending" Text="0"></asp:Label></span></a></li>--%>
                                                  
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    
                                </div>
                              

                        </div>
                        <!-- /.box -->
                    </div>
                       
        </div>
        </div>
                </section>
              <section class="content">
        
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h3 class="box-title"><asp:Label runat="server" ID="lblrpt"></asp:Label></h3>
                         
                          
                            </div>
                            <div class="box-body table-responsive ">
                                <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                    <th>SKU</th>
                                        <th>Rack Barcode</th>
                                        <th>MRP</th>
                                        <th>Count</th>
                                   
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("sku")%> </td>
                                                    <td><%# Eval("RackBarcode")%> </td>
                                                    <td><%# Eval("mrp")%> </td>
                                                    <td><%# Eval("cnt")%> </td>
                                           

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>



                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>


                                 
                            </div>
                        </div>
                    </div>
                </div>


     </section>
            
         </div>
</asp:Content>

