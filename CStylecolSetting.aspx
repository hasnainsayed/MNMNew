<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CStylecolSetting.aspx.cs" Inherits="CStylecolSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

    <div class="content-wrapper"> <section class="content-header">
        <h1>Data Field Check Setting
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="invoice.aspx">Data Field Check Setting</a></li>
        </ol>
    </section>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
           <ContentTemplate>
    <section class="content">
              <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                   
                    <div class="box-header">
                        <h3 class="box-title">Data Field Check Setting</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                        <div class="box-body" runat="server" id="divbind" visible="false"> 
                            
                             <div class="box-header">
                        <h3 class="box-title">
                            <asp:Button ID="btnadd" runat="server" Text="Add New" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          
                    </div>

                            <div class="box-body">
                                    <table id="example11" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                           <tr>
                                   <td>Category</td>
                                   <td>Update Setting </td>
                                   
                                   
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptbind" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ItemCategory")%>
                                            <asp:Label ID="catid" runat="server" Text='<%# Eval("catid").ToString() %>' Visible="false"></asp:Label> 
                                                <asp:Label ID="lblcatname" runat="server" Text='<%# Eval("ItemCategory").ToString() %>' Visible="false"></asp:Label> 
                                            </td>
                                          
                                            <td>
                                                <asp:LinkButton ID="btnupdate" runat="server" OnClick="btnupdate_Click" CssClass="btn bg-olive btn-sm "><i class="fa fa-sliders"></i> Update</asp:LinkButton>
                                               
                                            </td>
                                           

                                        </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        <div class="box-body" id="divsave" runat="server" visible="false">
											<div class="box-header">
												<h3 class="box-title">Dynamic Data</h3>

											</div>
											<!-- /.box-header -->
											<div class="box-body">
                                               
												<div >
                                                    <label>Select Category</label>
                                                                <asp:DropDownList ID="drp_itemCategory" runat="server"  DataValueField="ItemCategoryID" DataTextField="ItemCategory" CssClass="form-control select2"></asp:DropDownList>
                                                    </div>
													<div class="col-md-12 col-xs-12 col-lg-12">
														<table class="table table-bordered table-striped">
															<tr>
                                   <td>Vertical</td>
                                   <td>Select</td>
                                   
                                   
                               </tr>
                            </thead>
                            <tbody>
                            <asp:Repeater ID="rptsave" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("SettingName")%>
                                            <asp:Label ID="StyleCSID" runat="server" Text='<%# Eval("StyleCSID").ToString() %>' Visible="false"></asp:Label> 
                                                
                                                <asp:Label ID="ColumnNo" runat="server" Text='<%# Eval("ColumnNo").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                          
                                            <td><asp:RadioButtonList ID="rblstytype" class="form-control" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Size="Medium" Font-Bold="True" >
                                                                                      <asp:ListItem Text="Mandatory" Value="mandatory"></asp:ListItem>
                                                                                      <asp:ListItem Text="Optional" Value="optinal"  style="margin-left: 15px;"></asp:ListItem>
                                                                                      <asp:ListItem Text="NA" Value="Na" Selected="True"  style="margin-left: 15px;"></asp:ListItem>
                                                                                 </asp:RadioButtonList>
                                                                    </td>
                                           

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
														</table>
													</div>

													
													<div class="box-body table-responsive ">
														<div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                            <asp:Button ID="btncelset"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btncelset_Click"/>
                                                            <asp:Button ID="btnsaveset"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnsaveset_Click"/>
                                                            


														</div>
													</div>
												</div>
											</div>
                        <div class="box-body" id="divupdate" runat="server" visible="false">
                  
                <div class="box-header">
												<h3 class="box-title">Category :  <asp:Label ID="catname" runat="server" Font-Bold="true" ForeColor="#cc0000" Text="" Visible="true"></asp:Label> </h3>

											</div>
                    <div class="box-body">
               
                             <div class="box-body table-responsive">
                         
                        <table id="example113" class="table table-bordered table-striped ">
                            <thead>
                               <tr>
                                   <td>Vertical</td>
                                   <td>Select</td>
                                   
                                   
                               </tr>
                            </thead>
                            <tbody>
                                 <asp:Label ID="categoryid" runat="server" Text="" Visible="false"></asp:Label> 
                                 <asp:Repeater ID="rptupdate" runat="server" OnItemDataBound="rptbind_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("SettingName")%>
                                            <asp:Label ID="colid" runat="server" Text='<%# Eval("colid").ToString() %>' Visible="false"></asp:Label> 
                                               
                                                <asp:Label ID="colname" runat="server" Text='<%# Eval("colname").ToString() %>' Visible="false"></asp:Label> 
                                                <asp:Label ID="mandatory" runat="server" Text='<%# Eval("mandatory").ToString() %>' Visible="false"></asp:Label> 
                                                <asp:Label ID="optinal" runat="server" Text='<%# Eval("optinal").ToString() %>' Visible="false"></asp:Label> 
                                                <asp:Label ID="Na" runat="server" Text='<%# Eval("Na").ToString() %>' Visible="false"></asp:Label> 
                                                 
                                            </td>
                                          
                                            <td><asp:RadioButtonList ID="rblstytype" class="form-control" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Size="Medium" Font-Bold="True" >
                                                                                      <asp:ListItem Text="Mandatory" Value="mandatory"></asp:ListItem>
                                                                                      <asp:ListItem Text="Optional" Value="optinal"  style="margin-left: 15px;"></asp:ListItem>
                                                                                      <asp:ListItem Text="NA" Value="Na"  style="margin-left: 15px;"></asp:ListItem>
                                                                                 </asp:RadioButtonList>
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
                          

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Update" OnClick="btnSave_Click"/>
                    </div>
                
                <!-- /.box -->
            </div>
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

