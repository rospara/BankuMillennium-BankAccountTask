# UnitTests naming convention 
Feature to be tested: 
Many suggests that it is better to simply write the feature to be tested because one 
is anyway using annotations to identify method as test methods. It is also recommended 
for the reason that it makes unit tests as alternate form of documentation and avoid code smells. 
Following is how tests in first example would read like if named using this technique:
- Is_Not_An_Adult_If_Age_Less_Than_18
- Fail_To_Withdraw_Money_If_Account_Is_Invalid
- Student_Is_Not_Admitted_If_Mandatory_Fields_Are_Missing