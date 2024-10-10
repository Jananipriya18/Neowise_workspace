// Sample product data
const products = [
    {
        id: 1,
        name: 'Classic White T-Shirt',
        price: 100.99,
        category: 'men',
        image: 'https://placehold.co/300x400',
        description: 'Premium cotton crew neck t-shirt',
        sizes: ['S', 'M', 'L', 'XL'],
        colors: ['white', 'black', 'gray']
    },
    {
        id: 2,
        name: 'Blue Denim Jeans',
        price: 49.99,
        category: 'men',
        image: 'https://placehold.co/300x400',
        description: 'Stylish denim jeans with a slim fit',
        sizes: ['M', 'L', 'XL'],
        colors: ['blue']
    },
    {
        id: 3,
        name: 'Summer Dress',
        price: 60.99,
        category: 'women',
        image: 'https://placehold.co/300x400',
        description: 'Lightweight summer dress with floral patterns',
        sizes: ['S', 'M', 'L'],
        colors: ['red', 'yellow', 'blue']
    },
    {
        id: 4,
        name: 'Kids Casual Set',
        price: 25.99,
        category: 'kids',
        image: 'https://placehold.co/300x400',
        description: 'Comfortable t-shirt and shorts set',
        sizes: ['XS', 'S', 'M'],
        colors: ['green', 'blue']
    }
];

// Cart functionality
let cart = [];

// DOM Elements
const productGrid = document.getElementById('productGrid');
const cartModal = document.getElementById('cartModal');
const productModal = document.getElementById('productModal');
const cartCount = document.getElementById('cartCount');
const cartItems = document.getElementById('cartItems');
const cartTotal = document.getElementById('cartTotal');
const categoryFilter = document.getElementById('categoryFilter');
const searchInput = document.getElementById('searchInput');
const priceFilter = document.getElementById('priceFilter');
const navToggle = document.getElementById('navToggle');
const navLinks = document.getElementById('navLinks');

// Event Listeners
document.addEventListener('DOMContentLoaded', () => {
    displayProducts(products);
    loadCart();
});

navToggle.addEventListener('click', () => {
    navLinks.classList.toggle('active');
});

categoryFilter.addEventListener('change', filterProducts);
priceFilter.addEventListener('change', filterProducts);
searchInput.addEventListener('input', filterProducts);

document.getElementById('cartBtn').addEventListener('click', toggleCart);
document.getElementById('closeCart').addEventListener('click', toggleCart);
document.getElementById('closeProduct').addEventListener('click', toggleProductModal);

document.getElementById('newsletterForm').addEventListener('submit', (e) => {
    e.preventDefault();
    alert('Thank you for subscribing!');
    e.target.reset();
});

// Product Display Functions
function displayProducts(productsToShow) {
    productGrid.innerHTML = productsToShow.map(product => `
        <div class="product-card" onclick="showProductDetails(${product.id})">
            <img src="${product.image}" alt="${product.name}">
            <div class="product-info">
                <h3>${product.name}</h3>
                <p class="price">$${product.price.toFixed(2)}</p>
                <button class="btn" onclick="event.stopPropagation(); addToCart(${product.id})">
                    Add to Cart
                </button>
            </div>
        </div>
    `).join('');
}

function filterProducts() {
    const category = categoryFilter.value;
    const searchTerm = searchInput.value.toLowerCase();

    const filteredProducts = products.filter(product => {
        const matchCategory = category === 'all' || product.category === category;
        const matchSearchTerm = product.name.toLowerCase().includes(searchTerm);
        return matchCategory && matchSearchTerm;
    });

    displayProducts(filteredProducts);
}

function filterProducts() {
    const category = categoryFilter.value;
    const priceRange = priceFilter.value;
    const searchTerm = searchInput.value.toLowerCase();

    const filteredProducts = products.filter(product => {
        const matchCategory = category === 'all' || product.category === category;
        const matchSearchTerm = product.name.toLowerCase().includes(searchTerm);

        // Handle price filtering
        let matchPrice = true;
        if (priceRange !== 'all') {
            const [min, max] = priceRange.split('-').map(Number);
            matchPrice = product.price >= min && product.price < (max || Infinity);
        }

        return matchCategory && matchSearchTerm && matchPrice;
    });

    displayProducts(filteredProducts);
}

// Show product details in a modal
function showProductDetails(productId) {
    const product = products.find(p => p.id === productId);
    if (!product) return;

    productModal.querySelector('#productDetails').innerHTML = `
        <h2>${product.name}</h2>
        <img src="${product.image}" alt="${product.name}">
        <p>${product.description}</p>
        <p class="price">$${product.price.toFixed(2)}</p>
        <p>Available Sizes: ${product.sizes.join(', ')}</p>
        <p>Available Colors: ${product.colors.join(', ')}</p>
        <button class="btn" onclick="addToCart(${product.id})">Add to Cart</button>
    `;

    productModal.style.display = 'block';
}

// Toggle Cart Function
function toggleCart() {
    // Toggle the display of the cart modal
    cartModal.style.display = cartModal.style.display === 'block' ? 'none' : 'block';
}


function toggleProductModal() {
    productModal.style.display = 'none';
}

// Cart Functions
function addToCart(productId) {
    const product = products.find(p => p.id === productId);
    const cartItem = cart.find(item => item.product.id === productId);

    if (cartItem) {
        cartItem.quantity++;
    } else {
        cart.push({ product, quantity: 1 });
    }

    saveCart();
    loadCart();
}

function removeFromCart(productId) {
    cart = cart.filter(item => item.product.id !== productId);
    saveCart();
    loadCart();
}

function loadCart() {
    cartItems.innerHTML = cart.map(item => `
        <div class="cart-item">
            <span>${item.product.name}</span>
            <span>Qty: ${item.quantity}</span>
            <span>$${(item.product.price * item.quantity).toFixed(2)}</span>
            <button class="btn" onclick="removeFromCart(${item.product.id})">Remove</button>
        </div>
    `).join('');

    const total = cart.reduce((sum, item) => sum + item.product.price * item.quantity, 0);
    cartTotal.textContent = total.toFixed(2);
    cartCount.textContent = cart.length;
}

function saveCart() {
    localStorage.setItem('cart', JSON.stringify(cart));
}

function toggleCart() {
    cartModal.style.display = cartModal.style.display === 'block' ? 'none' : 'block';
}
