<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ColNStySetting.aspx.cs" Inherits="ColNStySetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>DropDown Check Settings
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="invoice.aspx">DropDown Check Settings</a></li>
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
                        <h3 class="box-title">DropDown Check Settings</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                        <div class="box-body" runat="server" id="divbind" visible="false"> 
                            
                             <div class="box-header">
                        <h3 class="box-title">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary pull-left" OnClick="btnAdd_Click" />
                          
                    </div>

                            <div class="box-body">
                                    <table id="example111" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                               
                                                <th>Category</th>
                                                <th>Update</th>
                                                
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="GV" runat="server" >
                                                <ItemTemplate>
                                                    <tr> <asp:Label ID="catid" runat="server" Visible="false" Text='<%# Eval("catid")%>' ></asp:Label>
                                                        <td><%# Eval("ItemCategory")%> </td>
                                                        <asp:Label ID="lblcatname" runat="server" Text='<%# Eval("ItemCategory").ToString() %>' Visible="false"></asp:Label> 
                                                        <td> <asp:LinkButton ID="btnupdate" runat="server" OnClick="btnupdate_Click" CssClass="btn bg-olive btn-sm "><i class="fa fa-sliders"></i> Update</asp:LinkButton></td>
                                                           
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
																<th width="15%">Verticals</th>
                                                                <th>Type</th>
															</tr>
                                                             
															<asp:Repeater ID="rptcolsetting" runat="server" >
																<ItemTemplate>
																	<tr>
                                                                <td><%# Eval("SettingName")%>
                                                                <asp:Label ID="lbltabname" runat="server" Visible="false" Text='<%# Eval("TableName")%>' ></asp:Label>
                                                                    
                                                                     <asp:Label ID="lblcolidd" runat="server" Visible="false" Text='<%# Eval("CTSettingID")%>' ></asp:Label>
                                                                </td>
                                                                <td><asp:RadioButtonList ID="rblstytype" class="form-control" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Size="Medium" Font-Bold="True" >
                                                                                      <asp:ListItem Text="Mandatory" Value="mandatory"></asp:ListItem>
                                                                                      <asp:ListItem Text="Optional" Value="optinal"  style="margin-left: 15px;"></asp:ListItem>
                                                                                      <asp:ListItem Text="NA" Selected="True" Value="Na"  style="margin-left: 15px;"></asp:ListItem>
                                                                                 </asp:RadioButtonList>
                                                                    </td>
                                                            </tr>
																</ItemTemplate>
															</asp:Repeater>
														</table>
													</div>

													
													<div class="box-body table-responsive ">
														<div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-sm btn-success pull-right" OnClick="btnsave_Click"  />
                                                           <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger pull-right" OnClick="cancel_Click" />
															
                                                            


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
                         
                        <table id="example1113" class="table table-bordered table-striped ">
                            <thead>
                               <tr>
                                   <td>Vertical</td>
                                   <td>Select</td>
                                   
                                   
                               </tr>
                            </thead>
                            <tbody>
                                 <asp:Label ID="categoryid" runat="server" Text="" Visible="false"></asp:Label> 
                                <asp:Repeater ID="rptupdate" runat="server" OnItemDataBound="rptupdate_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>

                                            <td><%# Eval("SettingName")%>
                                            <asp:Label ID="lblcolidd" runat="server" Text='<%# Eval("colid").ToString() %>' Visible="false"></asp:Label> 
                                                
                                                <asp:Label ID="lbltabname" runat="server" Text='<%# Eval("tablename").ToString() %>' Visible="false"></asp:Label> 
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

                        
                        <asp:Button ID="btncalset"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btncalset_Click"/>
                        <asp:Button ID="btnupdarteset"  class="btn btn-success pull-right btn-round" runat="server" Text="Update" OnClick="btnupdarteset_Click"/>
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

