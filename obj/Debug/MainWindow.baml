   M S B A M L   `   `   `  ����J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MessangerClientMessangerClient  5   	   C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  5   	        MessangerClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MessangerClient     5   	   ;controls&clr-namespace:MessangerClient.Controls     5   	     � Title$  
BirdSender��5	   	   $	��450��6   $	��800��6)   $
��1200��65   $	��500��6E    � WindowStartupLocation$ CenterScreen=�6U    � AllowsTransparency .  � 
ResizeMode$ CanResizeWithGrip=�6�    � WindowStyle$
 None=�6�    � 	Resources 5
       �� Source�� 5   
   $ Controls/Dictionary1.xaml?�6   %    6B   .��5      ]� ���#F2F2F2.+�5   
   %   MessangerClient.Controls.TopBar     TopBarHeader$ 
BirdSender��6      
ResizeMode . $	��Top=�6F   $	��Top=�6^   6u   ]� 5   
   .+�5      &    MessangerClient.Controls.InfoBar  -   5        	  Name$	 infobar��5      $	��Top=�60   $	��Top=�6H   6_   *   $MessangerClient.Controls.ChannelList  5      -   5        
  Name$
 channellist��5   #   $
��Left=�68   $
��Left=�6N   6k   �� 5      $��1��6   $
��Left=�6   ���#E6E6E66Q   � 5      .+�5      �� -   5         ��Name$ viewer��5   J   $��0,0,0,60q�6   $	��Top=�61    
  scroll#��[  6Z   $��Disabled=�6~   $
��Auto=�6�   .��5      �� -   5         ��Name$ msgcon��5   "    �� Orientation$ Vertical=�62   ���#F2F2F25      5      +   %MessangerClient.Controls.UserEnterMsg  5      -   5          Name$ userentermsg��5   (   $y�Hidden=�6>   $��Bottom=�6R   6o   5      5      5      5      