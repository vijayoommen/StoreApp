   M S B A M L   `   `   `  ����F  BStoreApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StoreApp.UIStoreApp.UI  5   	       StoreApp.UI.Setup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:StoreApp.UI     5   	     � Title$  Setup��5   	   $	��450��6   $	��800��6$   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��40 �6   �� 5      $	��120 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $	��200 �6   �� 5      $��50 �6   .+�5   
   �� $��Product��6   $��1��6#   $��1��60    �� Padding$ 10q�6@   $	��Top=�6M   $��5q�6e   �� 5   
   -   5        $��
cmbProduct��5      $��1��6%   $��2��62   $��10q�6B   $��4q�6O   $	��Top=�6Z   .@�5      �� $��Please select...��6    �� 
IsSelected$
 True��66   �� 5      $��Bananas��6   �� 5      $��Rubber Bands��6   �� 5      $��Pepto Bismol��6   5      �� 5!   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��2��6;   $��5q�6K   $��Center=�6V   .+�5"      	� -   5         	� 	GroupName$ 	PromoType��5"      
 � Tag$ 0��60   $
��None��68   $��5q�6G   6R   	� 5#      -   5        $ 	PromoType��5#      $ 1��60   $��
Percentage��68   $��5q�6M   6X   	� 5$      -   5        $ 	PromoType��5$      $ 2��60   $
��Bogo��68   $��5q�6G   6R   5%      �� 5'   
   -   5        $��PercentOptions��5'   q   $ 
Horizontal=�6   $��3��6.   $��2��6;   $��5q�6K   $��Center=�6V   $y�Hidden=�6�   .+�5(      �� $��Percentage Discount : ��6   �� 5)      -   5        $��10��5)      $��60��6     �� TextAlignment$ Right=�6+   6A   �� 5*      $��%��6   5+      �� 5-   
   -   5        $��BogoOptions��5-   q   $ 
Horizontal=�6   $��3��6.   $��2��6;   $��5q�6K   $��Center=�6V   $y�Hidden=�6�   .+�5.      �� $
��Buy ��6   �� 5/      -   5        $��10��5/      $��60��6    $ Right=�6+   6A   �� 50      $�� Get ��6   �� 51      -	   5        $��10��51      $��60��6    $ Right=�6+   6A   52      �� 54   
   $��4��6   $��3��6"   $ 
Horizontal=�62   .+�55      �� -
   5        $��5q�55   (   $	��150��63   .��6?   Save6E   �� 56      -   5        $��5q�56   )   $	��150��64   .��6@   Close6G   57      58      59      