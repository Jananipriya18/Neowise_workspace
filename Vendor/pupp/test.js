const puppeteer = require('puppeteer');

(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page1 = await browser.newPage();
// Test Case: Check if table body is present
try {
  await page1.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewVendors');

  await page1.waitForSelector('table tbody tr', { timeout: 5000 });
  
      // Check if there are at least some rows in the table
  
      const rowCount = await page1.$$eval('table tbody tr', rows => rows.length, { timeout: 5000 });
  
      // console.log(rowCount);
      if (rowCount > 0) 
      {
        console.log('TESTCASE:Vendors_table_rows_exist:success');
      } 
      else 
      {
        console.log('TESTCASE:Vendors_table_rows_exist:failure');
      }
    } 
    catch (e) 
    {
        console.log('TESTCASE:Vendors_table_rows_exist:failure');
    }

  

  const page2 = await browser.newPage();
try {
  await page2.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewVendor');

  // Test Case: Check if form exists and specific input fields are present
  const formExists = await page2.evaluate(() => {
    const form = document.querySelector('form');
    const inputFields = ['name', 'productOfferings', 'experience', 'storeLocation', 'operatingHours', 'phoneNumber'];
    const formHasInputFields = inputFields.every(field => !!form.querySelector(`[name="${field}"]`));
    return !!form && formHasInputFields;
  });

  if (formExists) {
    console.log('TESTCASE:CreateVendor_form_exists_and_input_fields_present_in_Create_Vendor:success');
  } else {
    console.log('TESTCASE:CreateVendor_form_exists_and_input_fields_present_in_Create_Vendor:failure');
  }

} catch (e) {
  console.log('TESTCASE:CreateVendor_form_exists_and_input_fields_present_in_Create_Vendor:failure');
}

const page3 = await browser.newPage();
  // Test Case: Check if table headers are present

try {
  await page3.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewVendors');

  // Wait for the table to be rendered
  // await page3.waitForSelector('table');
  await page3.waitForSelector('table', { timeout: 3000 });

  const tableHeaderContent = await page3.evaluate(() => {
    const thElements = Array.from(document.querySelectorAll('table th'));
    return thElements.map(th => th.textContent.trim());
  });

  const expectedHeaders = ['Name', 'Product Offerings', 'Experience', 'Store Location', 'Operating Hours', 'Phone Number'];

  const headerMatch = expectedHeaders.every(header => tableHeaderContent.includes(header));

  if (headerMatch) {
    console.log('TESTCASE:Vendor_table_header_content:success');
  } else {
    console.log('TESTCASE:Vendor_table_header_content:failure');
  }
} catch (e) {
  console.log('TESTCASE:Vendor_table_header_content:failure');
}

const page4 = await browser.newPage();
try {
  await page4.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewVendors');
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
  await page5.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewVendor');
  await page5.setViewport({
    width: 1200,
    height: 1200,
  });
  
  // Wait for the form elements to load
  await page5.waitForSelector('#name', { timeout: 1000 });
  await page5.waitForSelector('#productOfferings', { timeout: 1000 });
  await page5.waitForSelector('#experience', { timeout: 1000 });
  await page5.waitForSelector('#storeLocation', { timeout: 1000 });
  await page5.waitForSelector('#operatingHours', { timeout: 1000 });
  await page5.waitForSelector('#phoneNumber', { timeout: 1000 });
  await page5.waitForSelector('button[type="submit"]', { timeout: 1000 });

  // Click the Add Vendor button without entering any data
  await page5.click('button[type="submit"]');

  // Wait for a short period for validation to take effect
  await page5.waitForTimeout(1000);
    
  // Define an array of field brands and their corresponding error messages
  const fieldsToCheck = [
    { id: '#name', message: 'Name is required' },
    { id: '#productOfferings', message: 'Product Offerings is required' },
    { id: '#experience', message: 'Experience is required' },
    { id: '#storeLoction', message: 'Store Location is required' },
    { id: '#operatingHours', message: 'Operating Hours is required' },
    { id: '#phoneNumber', message: 'Phone Number is required' }
  ];
  

  let isValidationFailed = false;

  // Iterate through each field and check its corresponding error message
  for (const fieldData of fieldsToCheck) {
    const errorMessage = await page5.$eval(fieldData.id + ' + .error-message', el => el.textContent);
    if (!errorMessage.includes(fieldData.message)) {
      isValidationFailed = true;
      // console.log(`Validation message for ${fieldData.message} field: failure - Expected message: ${fieldData.message}`);
    }
  }

  // Log a single failure message if any validation fails
  if (isValidationFailed) {
    console.log('TESTCASE:Verify_required_validation_on_Add_Vendor_button:failure');
  } else {
    console.log('TESTCASE:Verify_required_validation_on_Add_Vendor_button:success');
  }
} catch (error) {
  console.log('TESTCASE:Verify_required_validation_on_Add_Vendor_button:failure');
}


const page6 = await browser.newPage();
try {
  // Navigate to add new event page
  await page6.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewVendor');
  await page6.setViewport({
    width: 1200,
    height: 1200,
  });
  console.log(page6.url());
  // Fill out the event creation form
  await page6.type('#name', 'Test Vendor 1');
  await page6.type('#productOfferings', 'Test Vendor ProductOfferings 1');
  await page6.type('#experience', '3 years');
  await page6.type('#storeLocation', 'India');
  await page6.type('#operatingHours', '10:00 AM');
  await page6.type('#phoneNumber', 'Test 9876543210');
  console.log("Submitted");
  // Submit the form
  await page6.click('button[type="submit"]');

  // Wait for a short period for the event to be added (optional)
  await page6.waitForTimeout(1000);

  const urlAfterClick = page6.url();
  console.log(urlAfterClick);
  if (urlAfterClick.toLowerCase().includes('viewvendors')) {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:success');
  } else {  
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
  }
} catch (e) {
  console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
}  

const page7 = await browser.newPage();
try {
  // Navigate to add new event page
  await page7.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewVendors');
  await page7.setViewport({
    width: 1200,
    height: 1200,
  });
  
  // Perform search for the newly added event
  await page7.waitForSelector('#search', { timeout: 5000 });
  await page7.type('#search', 'Test Vendor 1');
  await page7.click('.search-button');

  // Wait for the search results to load
  await page7.waitForSelector('.vendor-table', { timeout: 5000 });

  // Evaluate the search results
  const vendorNames = await page7.evaluate(() => {
    const eventRows = Array.from(document.querySelectorAll('.vendor-item'));
    return eventRows.map(row => row.querySelector('td:first-child').textContent.trim());
  });

  // Check if the searched event is found and matches exactly
  if (vendorNames.includes('Test Vendor 1')) {
    // && vendorNames.length === 1
    console.log('TESTCASE:Search_vendors_by_name:success');
  } else {
    console.log('TESTCASE:Search_vendors_by_name:failure');
  }

} catch (e) {
  console.log('TESTCASE:Search_vendors_by_name:failure');
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

