<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LRReport.aspx.cs" Inherits="LRReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
            <h1>LR 
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                 <li><a href="reportModule.aspx"><i class="fa fa-dashboard"></i>Report</a></li>
                <li class="active"><a  href="hotelFR.aspx">LR Report  </a></li>
               
            </ol>
        </section>
     
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>

    <div runat="server" id="divTasks">

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

                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>LR</label>
                                           <asp:DropDownList ID="drplr" runat="server" DataTextField="lrno" DataValueField="id" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                 
                                </div>
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
                                                    <li><a href="#" style="background: white; cursor: default">Trader Count <span class="pull-right badge bg-blue">
                                                        <asp:Label runat="server" ID="lbl1" Text="0"></asp:Label></a></span></li>
                                                    <%--  <li><a href="#">Tasks <span class="pull-right badge bg-aqua">5</span></a></li>--%>
                                                    <li><a href="#" style="background: white; cursor: default">Barcode Count <span class="pull-right badge bg-red"> 
                                                        <asp:Label runat="server" ID="lbl2" Text="0"></asp:Label></span></a></li>
                                                    <%--<li><a href="#" style="background: white; cursor:default">Pending Task   <span class="pull-right badge bg-red"><asp:Label runat="server" ID="lblpending" Text="0"></asp:Label></span></a></li>--%>
                                                  
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    
                                </div>
                              

                            <div class="box-footer pull-right">
                                    <asp:LinkButton runat="server" CssClass="btn btn-sm  btn-danger" ID="btnget" OnClick="btnget_Click"><i class="fa fa-database"></i> Get Report </asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-sm bg-olive-active" ID="refresh" OnClick="refresh_Click"><i class="fa fa-refresh"></i> Clear </asp:LinkButton>
                             </div>

                        </div>
                        <!-- /.box -->
                    </div>
                       
        </div>
        </div>
                </div>
             <div class="col-md-12">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h3 class="box-title"><asp:Label runat="server" ID="lblrpt"></asp:Label></h3>
                         
                          
                            </div>
                            <div class="box-body table-responsive ">
                                <table class="table table-hover table-bordered table-stripped">

                                    <tr>
                                        <th>Invoice Type</th>
                                        <th>Bag Description</th>
                                       
                                    </tr>

                                    <tbody>
                                        <asp:Repeater ID="GV" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("invType")%> </td>
                                                    <td><%# Eval("BagDescription")%> </td>
                                                    
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
            </ContentTemplate>
       
        </asp:UpdatePanel>
</asp:Content>

