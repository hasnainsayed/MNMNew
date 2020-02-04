<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="subLocationaspx.aspx.cs" Inherits="subLocationaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
            <section class="content-header">
            <h1>Sub Location
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                
                <li class="active"><a  href="physicalLoc.aspx">Physical Location</a></li>
                 <li>Sub Location</li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                 <div class="box box-primary" id="devCapone" runat="server" visible="false">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3><div id="divFormErr" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>

                                        </div>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
               
                         <div class="row">                      
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Sub Location Name</label>
                                
                                  <asp:TextBox ID="Location" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                            
                    </div>
                    
                <!-- /.box-body -->
                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <!-- /.box -->
            </div>
            </div>
                                    <section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            
            <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label> 
                <asp:Label ID="physicalID" runat="server" Text="" Visible="false"></asp:Label> 
                
                <div class="box">
                    <div class="box-header"><asp:Label ID="physicalLocation" runat="server" Text="" style="margin-bottom:10px;color:brown;font-weight:800"></asp:Label>
                        <h3 class="box-title btn-group pull-right">

                            
                          
                            <asp:Button ID="btnadd" runat="server" Text="Add New Sub Loaction" class="btn btn-primary btn-sm" OnClick="btnadd_Click" />

                            <asp:LinkButton ID="physical" runat="server" CssClass="btn btn-sm btn-danger" href="physicalLoc.aspx">Go to Physical Location</asp:LinkButton>
                        </h3>
                     <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>

                                        </div>

                                        <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Updated</h5>
                                        </div>

                        <div id="divErrorAlert" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Transaction Failed</h5>

                                        </div>      
                    
                    </div>
                    <div class="box-body table-responsive">
                        
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                                <tr>
                                   <th>ID</th>                                   
                                   <th>Sub Location</th>                                   
                                   <th>Total Space </th>
                                   <th>Stock Count </th>
                                    <th>Available Space </th>
                                   <th>Add</th>
                                   <th>Action</th>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server" OnItemDataBound="GV_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("SublocationID")%>
                                            <asp:Label ID="SublocationID" runat="server" Text='<%# Eval("SublocationID").ToString() %>' Visible="false"></asp:Label> </td>
                                           
                                            <td><asp:Label ID="Sublocation" runat="server" Text='<%# Eval("Sublocation").ToString() %>' Visible="false"></asp:Label><%# Eval("Sublocation")%></td> 
                                            
                                            <td><asp:Label ID="totalSpace" runat="server" ></asp:Label></td>
                                            <td><%# Eval("stockCnt")%></td>
                                            <td><asp:Label ID="spaceAvailable" runat="server" ></asp:Label></td>
                                           <td> 
                                               <asp:LinkButton ID="btnAddRack" runat="server" CssClass="btn btn-warning btn-sm" OnClick="btnAddRack_Click" ><i class="fa fa-home"></i> Rack</asp:LinkButton>
                                               
                                           </td>
                                            <td>
                                                <asp:LinkButton ID="btnEditSubLoc" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnEditSubLoc_Click" ><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
                            </asp:UpdatePanel>
    </div>
</asp:Content>

