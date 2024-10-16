const puppeteer = require('puppeteer');

(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page1 = await browser.newPage();
// Test Case: Check if table body is present
try {
  await page1.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewshops');

  await page1.waitForSelector('table tbody tr', { timeout: 5000 });
  
      // Check if there are at least some rows in the table
  
      const rowCount = await page1.$$eval('table tbody tr', rows => rows.length, { timeout: 5000 });
  
      // console.log(rowCount);
      if (rowCount > 0) 
      {
        console.log('TESTCASE:CheeseShops_table_rows_exist:success');
      } 
      else 
      {
        console.log('TESTCASE:CheeseShops_table_rows_exist:failure');
      }
    } 
    catch (e) 
    {
        console.log('TESTCASE:CheeseShops_table_rows_exist:failure');
    }

  

  const page2 = await browser.newPage();
try {
  await page2.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewShop');

  // Test Case: Check if form exists and specific input fields are present
  const formExists = await page2.evaluate(() => {
    const form = document.querySelector('form');
    const inputFields = ['ownerName', 'cheeseSpecialties', 'experienceYears', 'storeLocation', 'importedCountry', 'phoneNumber'];
    const formHasInputFields = inputFields.every(field => !!form.querySelector(`[name="${field}"]`));
    return !!form && formHasInputFields;
  });

  if (formExists) {
    console.log('TESTCASE:CreateCheeseShop_form_exists_and_input_fields_present_in_Create_CheeseShop:success');
  } else {
    console.log('TESTCASE:CreateCheeseShop_form_exists_and_input_fields_present_in_Create_CheeseShop:failure');
  }

} catch (e) {
  console.log('TESTCASE:CreateCheeseShop_form_exists_and_input_fields_present_in_Create_CheeseShop:failure');
}

const page3 = await browser.newPage();
  // Test Case: Check if table headers are present

try {
  await page3.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewshops');

  // Wait for the table to be rendered
  // await page3.waitForSelector('table');
  await page3.waitForSelector('table', { timeout: 3000 });

  const tableHeaderContent = await page3.evaluate(() => {
    const thElements = Array.from(document.querySelectorAll('table th'));
    return thElements.map(th => th.textContent.trim());
  });

  const expectedHeaders = ['Owner Name', 'Cheese Specialties', 'Experience Years', 'Store Location', 'Imported Country', 'Phone Number'];

  const headerMatch = expectedHeaders.every(header => tableHeaderContent.includes(header));

  if (headerMatch) {
    console.log('TESTCASE:CheeseShops_table_header_content:success');
  } else {
    console.log('TESTCASE:CheeseShops_table_header_content:failure');
  }
} catch (e) {
  console.log('TESTCASE:CheeseShops_table_header_content:failure');
}

const page4 = await browser.newPage();
try {
  await page4.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewshops');
  await page4.setViewport({
    width: 1200,
    height: 1200,
  });
  await page4.waitForSelector('#delete', { timeout: 3000 }); // Wait for delete button

  await page4.click('#delete');

  await page4.waitForSelector('h2', { timeout: 2000 }); // Wait for confirmation message

  const confirmationMessage = await page4.$eval('h2', element => element.textContent.toLowerCase());

  const urlAfterClick = page4.url();

  if (confirmationMessage.includes("delete confirmation") && urlAfterClick.toLowerCase().includes('confirmdelete')) {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:success');
  } else {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
  }
} catch (error) {
  console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
}

const page5 = await browser.newPage();
try {
  await page5.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewShop');
  await page5.setViewport({
    width: 1200,
    height: 1200,
  });
  
  // Wait for the form elements to load
  await page5.waitForSelector('#ownerName', { timeout: 1000 });
  await page5.waitForSelector('#cheeseSpecialties', { timeout: 1000 });
  await page5.waitForSelector('#experienceYears', { timeout: 1000 });
  await page5.waitForSelector('#storeLocation', { timeout: 1000 });
  await page5.waitForSelector('#importedCountry', { timeout: 1000 });
  await page5.waitForSelector('#phoneNumber', { timeout: 1000 });
  await page5.waitForSelector('button[type="submit"]' , { timeout: 1000 });

  // Click the Add CheeseShops button without entering any data
  await page5.click('button[type="submit"]');

  // Wait for a short period for validation to take effect
  await page5.waitForTimeout(1000);
    
  // Define an array of field IDs and their corresponding error messages
  const fieldsToCheck = [
    { id: '#ownerName', message: 'Owner name is required' },
    { id: '#cheeseSpecialties', message: 'Cheese specialties are required' },
    { id: '#experienceYears', message: 'Experience years is required' },
    { id: '#storeLocation', message: 'Store location is required' },
    { id: '#importedCountry', message: 'Imported country is required' },
    { id: '#phoneNumber', message: 'Phone number is required' },
  ];

  let isValidationFailed = false;

  for (const fieldData of fieldsToCheck) {
        const errorMessage = await page5.$eval(fieldData.id + ' + .error', el => el.textContent);
        if (!errorMessage.includes(fieldData.message)) {
          isValidationFailed = true;
        }
        console.log(errorMessage);
      }

  // Log a single failure message if any validation fails
  if (isValidationFailed) {
    console.log('TESTCASE:Verify_required_validation_on_Add_CheeseShops_button:failure');
  } else {
    console.log('TESTCASE:Verify_required_validation_on_Add_CheeseShops_button:success');
  }
} catch (error) {
  console.log('TESTCASE:Verify_required_validation_on_Add_CheeseShops_button:failure');
}


const page6 = await browser.newPage();
try {
  // Navigate to the add new cheese shop page
  await page6.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewShop');
  await page6.setViewport({
    width: 1200,
    height: 1200,
  });
  console.log('Navigated to:', page6.url());

  // Fill out the cheese shop creation form
  await page6.type('#ownerName', 'Test Owner Name');
  await page6.type('#cheeseSpecialties', 'Test Cheese Specialties');
  await page6.type('#experienceYears', '10');
  await page6.type('#storeLocation', 'Test Store Location');
  await page6.type('#importedCountry', 'Test Imported Country');
  await page6.type('#phoneNumber', '1234567890');
  console.log('Form filled out');

  // Submit the form
  await page6.click('button[type="submit"]');
  console.log('Form submitted');

  // Wait for navigation or URL change
  await page6.waitForNavigation({ waitUntil: 'networkidle0' });
  const urlAfterClick = page6.url();
  console.log('URL after form submission:', urlAfterClick);

  // Check if navigated to the view cheese shops page
  if (urlAfterClick.toLowerCase().includes('viewshops')) {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:success');
  } else {  
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
  }
} catch (e) {
  console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
}
 

const page7 = await browser.newPage();
try {
  // Step 1: Navigate to the add cheese shop page and insert dummy data
  await page7.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewShop');
  await page7.setViewport({
    width: 1200,
    height: 1200,
  });

  // Wait for the form elements to load
  await page7.waitForSelector('#ownerName' , { timeout: 1000 });
  await page7.waitForSelector('#cheeseSpecialties', { timeout: 1000 });
  await page7.waitForSelector('#experienceYears', { timeout: 1000 });
  await page7.waitForSelector('#storeLocation', { timeout: 1000 });
  await page7.waitForSelector('#importedCountry', { timeout: 1000 });
  await page7.waitForSelector('#phoneNumber', { timeout: 1000 });
  await page7.waitForSelector('button[type="submit"]', { timeout: 1000 });

  // Fill out the form with dummy data
  await page7.type('#ownerName', 'John Cheese');
  await page7.type('#cheeseSpecialties', 'Cheddar, Gouda');
  await page7.type('#experienceYears', '15');
  await page7.type('#storeLocation', 'Cheese Street');
  await page7.type('#importedCountry', 'France');
  await page7.type('#phoneNumber', '1234567890');

  // Submit the form
  await page7.click('button[type="submit"]');

  // Wait for the data to be processed and page to reload
  await page7.waitForTimeout(2000); // Adjust this timeout as necessary

  // Perform search for the last letter of OwnerName
  await page7.waitForSelector('#searchBox', { timeout: 5000 });
  
  // Search for the last letter of the OwnerName
  await page7.type('#searchBox', 'e'); // Search for the letter 'e'
  await page7.click('#searchButton');

  // Wait for the search results to load
  await page7.waitForTimeout(2000); // Ensure the search results have time to load
  
  // Evaluate the search results
  const ownerNames = await page7.evaluate(() => {
    const shopRows = Array.from(document.querySelectorAll('.cheese-shop-item'));
    return shopRows.map(row => row.querySelector('td:first-child').textContent.trim());
  });

  // Log the owner names found
  console.log('Owner Names Found:', ownerNames);

  // Check if the search results include shops with the last letter 'e'
  const expectedSearchTerm = 'e'; // The letter used for searching
  const searchResults = ownerNames.some(name => name.toLowerCase().includes(expectedSearchTerm.toLowerCase()));

  if (searchResults) {
    console.log('TESTCASE:Search_shops_by_name:success');
  } else {
    console.log('TESTCASE:Search_shops_by_name:failure');
  }

} catch (e) {
  console.log('TESTCASE:Search_shops_by_name:failure');
}



  finally{
    await page1.close();
    await page2.close();
    await page3.close();
    await page4.close();
    await page5.close();
    await page6.close();
    await page7.close();
  }
 

  await browser.close();
})();
