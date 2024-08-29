
const puppeteer = require('puppeteer');

(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    args: [
      "--headless",
      "--disable-gpu",
      "--remote-debugging-port=9222",
      "--no-sandbox",
      "--disable-setuid-sandbox",
    ],
  });


  const page8 = await browser.newPage();
  try {
    await page8.goto('https://8081-ddadcdbccbabeef314787991aefaeccceedbone.premiumproject.examly.io/'); // Replace with your actual test page8 URL.
    await page8.setViewport({
      width: 1200,
      height: 800,
    });
    const buttonStyles = await page8.$eval('button', (button) => {
      return {
        backgroundColor: getComputedStyle(button).backgroundColor,
        fontSize: getComputedStyle(button).fontSize,
      };
    });
    if (buttonStyles.backgroundColor === 'rgb(0, 123, 255)' && buttonStyles.fontSize === '16px') {
      console.log('TESTCASE:button_background_color_and_font_size_is_correct:success');
    } else {
      console.log('TESTCASE:button_background_color_and_font_size_is_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:button_background_color_and_font_size_is_correct:failure');
  }
  // TestCase: Check if the link element has the correct color and font size
  const page9 = await browser.newPage();
  try {
    await page9.goto('https://8081-ddadcdbccbabeef314787991aefaeccceedbone.premiumproject.examly.io/'); // Replace with your actual test page9 URL.
    await page9.setViewport({
      width: 1200,
      height: 800,
    });
    const linkStyles = await page9.$eval('a', (link) => {
      return {
        color: getComputedStyle(link).color,
        fontSize: getComputedStyle(link).fontSize,
      };
    });

    if (linkStyles.color === 'rgb(51, 51, 51)' && linkStyles.fontSize === '16px') {
      console.log('TESTCASE:link_color_and_font_size_is_correct:success');
    } else {
      console.log('TESTCASE:link_color_and_font_size_is_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:link_color_and_font_size_is_correct:failure');

  }
  // TestCase: Check if the label element has the correct font size and margin-right

  // TestCase: Check if the h2 element has the correct font size, text color, and text alignment
  const page12 = await browser.newPage();
  try {
    await page12.goto('https://8081-ddadcdbccbabeef314787991aefaeccceedbone.premiumproject.examly.io/'); // Replace with your actual test page12 URL.
    await page12.setViewport({
      width: 1200,
      height: 800,
    });

    const h2Styles = await page12.$eval('h2', (h2) => {
      return {
        fontSize: getComputedStyle(h2).fontSize,
        color: getComputedStyle(h2).color,
        textAlign: getComputedStyle(h2).textAlign,
      };
    });

    if (
      h2Styles.fontSize === '28px' &&
      h2Styles.color === 'rgb(51, 51, 51)' &&
      h2Styles.textAlign === 'center'
    ) {
      console.log('TESTCASE:h2_element_styles_is_correct:success');
    } else {
      console.log('TESTCASE:h2_element_styles_is_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:h2_element_styles_is_correct:failure');

  }
  // TestCase: Check if the input[type="text"] element has the correct width, padding, and font size
  const page13 = await browser.newPage();
  try {
    await page13.goto('https://8081-ddadcdbccbabeef314787991aefaeccceedbone.premiumproject.examly.io/'); // Replace with your actual test page13 URL.
    await page13.setViewport({
      width: 1200,
      height: 800,
    });
    const inputStyles = await page13.$eval('input[type="text"]', (input) => {
      return {
        padding: getComputedStyle(input).padding,
        fontSize: getComputedStyle(input).fontSize,
      };
    });

    const expectedInputStyles = { padding: '10px', fontSize: '16px' }
    const equalsCheck = (inputStyles, expectedInputStyles) => {
        return JSON.stringify(inputStyles) === JSON.stringify(expectedInputStyles);
    }
    
    if (equalsCheck(inputStyles, expectedInputStyles)){ 
      console.log('TESTCASE:input_text_styles_are_correct:success');
    } else {
      console.log('TESTCASE:input_text_styles_are_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:input_text_styles_are_correct:failure');

  }
  // TestCase: Check if the button element has the correct background color and font size
  const page14 = await browser.newPage();
  try {
    await page14.goto('https://8081-ddadcdbccbabeef314787991aefaeccceedbone.premiumproject.examly.io/'); // Replace with your actual test page14 URL.
    await page14.setViewport({
      width: 1200,
      height: 800,
    });
    const buttonStyles = await page14.$eval('button', (button) => {
      return {
        backgroundColor: getComputedStyle(button).backgroundColor,
        fontSize: getComputedStyle(button).fontSize,
      };
    });
    if (
      buttonStyles.backgroundColor === 'rgb(0, 123, 255)' &&
      buttonStyles.fontSize === '16px'
    ) {
      console.log('TESTCASE:button_styles_are_correct:success');
    } else {
      console.log('TESTCASE:button_styles_are_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:button_styles_are_correct:failure');
  }

  // TestCase: Check if the a elements have the correct color and text decoration
  const page17 = await browser.newPage();
  try {
    await page17.goto('https://8081-ddadcdbccbabeef314787991aefaeccceedbone.premiumproject.examly.io/'); // Replace with your actual test page17 URL.
    await page17.setViewport({
      width: 1200,
      height: 800,
    });
    const aStyles = await page17.$eval('a', (a) => {
      return {
        color: getComputedStyle(a).color,
        textDecoration: getComputedStyle(a).textDecoration,
      };
    });

    if (
      aStyles.color === 'rgb(51, 51, 51)' &&
      aStyles.textDecoration === 'none solid rgb(51, 51, 51)'
    ) {
      console.log('TESTCASE:a_tag_styles_are_correct:success');
    } else {
      console.log('TESTCASE:a_tag_styles_are_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:a_tag_styles_are_correct:failure');
  } finally {
    await page8.close();
    await page9.close();
    await page12.close();
    await page13.close();
    await page14.close();
    await page17.close();
    await browser.close();
  }
})();
