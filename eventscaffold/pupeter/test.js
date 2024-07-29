const puppeteer = require('puppeteer');

(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page1 = await browser.newPage();
// Test Case: Check if table body is present
try {
  await page1.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewEpisodes');

  await page1.waitForSelector('table tbody tr', { timeout: 5000 });
  
      // Check if there are at least some rows in the table
  
      const rowCount = await page1.$$eval('table tbody tr', rows => rows.length, { timeout: 5000 });
  
      // console.log(rowCount);
      if (rowCount > 0) 
      {
        console.log('TESTCASE:CartoonEpisodes_table_rows_exist:success');
      } 
      else 
      {
        console.log('TESTCASE:CartoonEpisodes_table_rows_exist:failure');
      }
    } 
    catch (e) 
    {
        console.log('TESTCASE:CartoonEpisodes_table_rows_exist:failure');
    }

  

  const page2 = await browser.newPage();
try {
  await page2.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewEpisode');

  // Test Case: Check if form exists and specific input fields are present
  const formExists = await page2.evaluate(() => {
    const form = document.querySelector('form');
    const inputFields = ['cartoonSeriesName', 'episodeTitle', 'releaseDate', 'directorName', 'duration', 'description'];
    const formHasInputFields = inputFields.every(field => !!form.querySelector(`[name="${field}"]`));
    return !!form && formHasInputFields;
  });

  if (formExists) {
    console.log('TESTCASE:CreateCartoonEpisode_form_exists_and_input_fields_present_in_Create_CartoonEpisode:success');
  } else {
    console.log('TESTCASE:CreateCartoonEpisode_form_exists_and_input_fields_present_in_Create_CartoonEpisode:failure');
  }

} catch (e) {
  console.log('TESTCASE:CreateCartoonEpisode_form_exists_and_input_fields_present_in_Create_CartoonEpisode:failure');
}

const page3 = await browser.newPage();
  // Test Case: Check if table headers are present

try {
  await page3.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewEpisodes');

  // Wait for the table to be rendered
  // await page3.waitForSelector('table');
  await page3.waitForSelector('table', { timeout: 3000 });

  const tableHeaderContent = await page3.evaluate(() => {
    const thElements = Array.from(document.querySelectorAll('table th'));
    return thElements.map(th => th.textContent.trim());
  });

  const expectedHeaders = ['Cartoon Series Name', 'Episode Title', 'Release Date', 'Director Name', 'Duration', 'Description'];

  const headerMatch = expectedHeaders.every(header => tableHeaderContent.includes(header));

  if (headerMatch) {
    console.log('TESTCASE:CartoonEpisodes_table_header_content:success');
  } else {
    console.log('TESTCASE:CartoonEpisodes_table_header_content:failure');
  }
} catch (e) {
  console.log('TESTCASE:CartoonEpisodes_table_header_content:failure');
}

const page4 = await browser.newPage();
try {
  await page4.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewEpisodes');
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
  console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure', error);
}

const page5 = await browser.newPage();
try {
  await page5.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewEpisode');
  await page5.setViewport({
    width: 1200,
    height: 1200,
  });
  
  // Wait for the form elements to load
  await page5.waitForSelector('#cartoonSeriesName');
  await page5.waitForSelector('#episodeTitle');
  await page5.waitForSelector('#releaseDate');
  await page5.waitForSelector('#directorName');
  await page5.waitForSelector('#duration');
  await page5.waitForSelector('#description');
  await page5.waitForSelector('button[type="submit"]');

  // Click the Add CartoonEpisodes button without entering any data
  await page5.click('button[type="submit"]');

  // Wait for a short period for validation to take effect
  await page5.waitForTimeout(1000);
    
  // Define an array of field brands and their corresponding error messages
  const fieldsToCheck = [
    { id: '#cartoonSeriesName', message: 'Cartoon series name  is required' },
    { id: '#episodeTitle', message: 'Episode title is required' },
    { id: '#releaseDate', message: 'Release date  is required' },
    { id: '#directorName', message: 'Director name is required' },
    { id: '#duration', message: 'Duration is required' },
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
    console.log('TESTCASE:Verify_required_validation_on_Add_CartoonEpisodes_button:failure');
  } else {
    console.log('TESTCASE:Verify_required_validation_on_Add_CartoonEpisodes_button:success');
  }
} catch (error) {
  console.log('TESTCASE:Verify_required_validation_on_Add_CartoonEpisodes_button:failure');
}


const page6 = await browser.newPage();
try {
  // Navigate to add new event page
  await page6.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addNewEpisode');
  await page6.setViewport({
    width: 1200,
    height: 1200,
  });
  console.log(page6.url());
  // Fill out the event creation form
  await page6.type('#cartoonSeriesName', 'Test CartoonEpisodes name');
  await page6.type('#episodeTitle', 'Test CartoonEpisodes title');
  await page6.type('#releaseDate', '2024-07-10');
  await page6.type('#directorName', 'Test Director name');
  await page6.type('#duration', 'Test Duration');
  console.log("Submitted");
  // Submit the form
  await page6.click('button[type="submit"]');

  // Wait for a short period for the event to be added (optional)
  await page6.waitForTimeout(1000);

  const urlAfterClick = page6.url();
  console.log(urlAfterClick);
  if (urlAfterClick.toLowerCase().includes('viewevents')) {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:success');
  } else {  
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
  }
} catch (e) {
  console.log('TESTCASE:Add_navigates_to_Search:failure', e);
}  

const page7 = await browser.newPage();
try {
  // Navigate to add new event page
  await page7.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/viewEpisodes');
  await page7.setViewport({
    width: 1200,
    height: 1200,
  });
  
  // Perform search for the newly added event
  await page7.waitForSelector('#search', { timeout: 5000 });
  await page7.type('#search', 'Test CartoonEpisodes 1');
  await page7.click('.search-button');

  // Wait for the search results to load
  await page7.waitForSelector('.event-table', { timeout: 5000 });

  // Evaluate the search results
  const playlistNames = await page7.evaluate(() => {
    const eventRows = Array.from(document.querySelectorAll('.cartoon-episode-item'));
    return eventRows.map(row => row.querySelector('td:first-child').textContent.trim());
  });

  // Check if the searched event is found and matches exactly
  if (playlistNames.includes('Test CartoonEpisodes 1')) {
    // && playlistNames.length === 1
    console.log('TESTCASE:Search_events_by_name:success');
  } else {
    console.log('TESTCASE:Search_events_by_name:failure');
  }

} catch (e) {
  console.log('TESTCASE:Search_events_by_name:failure');
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

