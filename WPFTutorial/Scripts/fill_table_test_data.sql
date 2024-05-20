INSERT INTO [User] (
    [DateOfBirth],
    [Discriminator], 
    [Email],
    [Gender],
    [Name],
    [Surname],
    [Password],
    [Role]
) VALUES (
    '2001-01-01',
    'Teacher',
    'testTeacher@gmail.com',
    'helicopter',
    'Test',
    'Testovic',
    'test123',
    1
);
