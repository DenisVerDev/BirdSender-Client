   M S B A M L   `   `   `  ����J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MessangerClientMessangerClient  5   	   C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  5   	   "    MessangerClient.GroupCreator   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MessangerClient     5   	   ;controls&clr-namespace:MessangerClient.Controls     5   	   b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/  5   	     � Title$  Create group��5
   	   $	��600��6   $	��450��6+    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$
 None=�6\    � 
ResizeMode$ NoResize=�6o    � ShowInTaskbar .   � 	Resources 5       �� Source�� 5   
   $ Controls/Dictionary1.xaml?�6   %    6B   .��5      �� ���#6CC4C7
�������$��1q�6:   ._�5   
   ]� .+�5      �� $	��Top=�6   .+�5      %   MessangerClient.Controls.TopBar  $	��Top=�6"      HideMode .     CanMove .  	   TopBarHeader$	 Create group��6[   
   
ResizeMode
 .  6�   �� 5      -   5         ��Name$ 	btnavatar��5      $��Center=�6,   $��0,10,0,0q�6I   $	��Top=�6\      btncircleempty#��[  6t   $	��100��6�   $	��100��6�   ��5      (  "SharpVectors.Converters.SvgViewbox  5      $��36��6*   $��36��65      Source$ Resources/btnphoto.svg?�6A   6c   5      *   $MessangerClient.Controls.BirdTextBox  5      -   5          Name$ tbname��5   '      	MaxLength$ 25��67   $	��400��6F   $��30��6R      DefaultText$ 
Group name��6^   $
��10pt(�6w   $��	0,20,0,20q�6�   6�   5      �� 5      
�������.+�5      *   $MessangerClient.Controls.ChannelList  -   5          Name$ 	userslist��5   '      MultiSelect . $	��450��6M   $	��330��6Y   6h   �� 5      -   5        $ 	btncreate��5        
birdbutton#��[ 6,   $��0,5,0,0q�6S   $��Center=�6d   ���#4ABFB6
�������$��Arial)�6�   $
��12pt(�6�   $	��122��6�   $��42��6�   $��Create group��6�   6  5      5      5      5      