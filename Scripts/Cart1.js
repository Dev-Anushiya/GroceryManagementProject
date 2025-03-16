//alert("hi")

let cart = {};

// Load cart from localStorage when the cart page is loaded
function loadCartFromStorage() {
    const storedCart = localStorage.getItem('cart');
    if (storedCart) {
        //cart = JSON.parse(storedCart);
        Object.assign(cart, JSON.parse(storedCart));
    }
}

// Check if cart data is available 
document.addEventListener('DOMContentLoaded', function () {

    loadCartFromStorage();
    displayCartItems();
});

// Function to display items in the cart on the View Cart page
function displayCartItems() {
    const cartContainer = document.getElementById('cart-items');
    const cartTotalElement = document.getElementById('cart-total-value');
    cartContainer.innerHTML = ''; // Clear any existing content

    let total = 0;

    // Check if cart is empty
    if (Object.keys(cart).length === 0) {
        cartContainer.innerHTML = '<p>No items in cart</p>';
        return;
    }

    // Display each item in the cart
    for (const [itemId, quantity] of Object.entries(cart)) {
        const itemData = getItemData(itemId); // Assuming function to retrieve item info based on ID

        const cartItem = document.createElement('div');
        cartItem.className = 'cart-item';
        cartItem.innerHTML = `
            <span><strong>${itemData.name}</strong></span> - 
            <span>Quantity: </span>
            <button class="btn btn-outline-danger btn-sm" onclick="decreaseCartItem('${itemId}')">-</button>
            <span>${quantity}</span>
            <button class="btn btn-outline-success btn-sm" onclick="increaseCartItem('${itemId}')">+</button>
            <span>Price: €${(itemData.price * quantity).toFixed(2)}</span>
            <button class="btn btn-secondary" onclick="removeCartItem('${itemId}')">Remove</button>
        `;

        cartContainer.appendChild(cartItem);

        // Calculate total price
        total += itemData.price * quantity;
    }

    // Update the total price display
    cartTotalElement.innerText = total.toFixed(2);
}

// Function to retrieve item data by ID (assuming you have item info stored)
function getItemData(itemId) {
    // Sample data lookup; replace this with actual data retrieval if needed
    const items = {
        "1": { name: "Organic Apples", price: 0.99 },
        "2": { name: "Whole Wheat Bread", price: 2.49 },
        "3": { name: "Free-Range Eggs", price: 3.99 },
        "4": { name: "Almond Milk", price: 2.99 },
        "5": { name: "Broccoli", price: 1.29 },
        "6": { name: "Chicken Breast", price: 5.99 },
        "7": { name: "Organic Brown Rice", price: 4.49 },
        "8": { name: "Greek Yogurt", price: 1.49 },
        "9": { name: "Honey", price: 6.99 },
        "10": { name: "Carrots", price: 0.89 }
        // Add more items as necessary
    };
    return items[itemId];
}

// Function to increase quantity in the cart
function increaseCartItem(itemId) {
    cart[itemId] += 1;
    updateCartStorage();
    displayCartItems();
}

// Function to decrease quantity in the cart
function decreaseCartItem(itemId) {
    if (cart[itemId] > 1) {
        cart[itemId] -= 1;
    } else {
        delete cart[itemId];
    }
    updateCartStorage();
    displayCartItems();
}

// Function to remove an item from the cart
function removeCartItem(itemId) {
    delete cart[itemId];
    updateCartStorage();
    displayCartItems();
}

// Helper function to update cart in storage (session/local storage)
function updateCartStorage() {
    localStorage.setItem('cart', JSON.stringify(cart));
}

function populateHiddenCartInputs(cart) {
    const hiddenInputsContainer = document.getElementById('hidden-cart-inputs');
    hiddenInputsContainer.innerHTML = ''; 

    //Object.entries(cart).forEach(([itemId, quantity]) => {
    //    const itemData = getItemData(itemId); // Retrieve item details by ID

    //    const itemIdInput = document.createElement('input');
    //    itemIdInput.type = 'hidden';
    //    itemIdInput.name = `cartItems[${itemId}].ItemId`; // For model binding
    //    itemIdInput.value = itemId;

    //    const itemQuantityInput = document.createElement('input');
    //    itemQuantityInput.type = 'hidden';
    //    itemQuantityInput.name = `cartItems[${itemId}].Quantity`; // For model binding
    //    itemQuantityInput.value = quantity;

    //    const itemPriceInput = document.createElement('input');
    //    itemPriceInput.type = 'hidden';
    //    itemPriceInput.name = `cartItems[${itemId}].Price`; // For model binding
    //    itemPriceInput.value = itemData.price;

    //    hiddenInputsContainer.appendChild(itemIdInput);
    //    hiddenInputsContainer.appendChild(itemQuantityInput);
    //    hiddenInputsContainer.appendChild(itemPriceInput);

    //});

    let index = 0; // Sequential index for proper model binding
    Object.entries(cart).forEach(([itemId, quantity]) => {
        const itemData = getItemData(itemId); // Retrieve item details by ID

        // Create inputs for each property
        const itemIdInput = document.createElement('input');
        itemIdInput.type = 'hidden';
        itemIdInput.name = `cartItems[${index}].ProductId`; // Sequential indexing
        itemIdInput.value = itemId;

        const itemQuantityInput = document.createElement('input');
        itemQuantityInput.type = 'hidden';
        itemQuantityInput.name = `cartItems[${index}].Quantity`; // Sequential indexing
        itemQuantityInput.value = quantity;

        const itemPriceInput = document.createElement('input');
        itemPriceInput.type = 'hidden';
        itemPriceInput.name = `cartItems[${index}].Price`; // Sequential indexing
        itemPriceInput.value = itemData.price;

        const itemProductInput = document.createElement('input');
        itemProductInput.type = 'hidden';
        itemProductInput.name = `cartItems[${index}].Product`; // Sequential indexing
        itemProductInput.value = itemData.name;

        // Append inputs to the hidden container
        hiddenInputsContainer.appendChild(itemIdInput);
        hiddenInputsContainer.appendChild(itemQuantityInput);
        hiddenInputsContainer.appendChild(itemPriceInput);
        hiddenInputsContainer.appendChild(itemProductInput);

        index++; // Increment index for the next item
    });

    // Update the total amount input
    var cartTotal = $('#cart-total-value').text();

    // Convert it to a number if needed
    var cartTotalValue = parseFloat(cartTotal);
    document.getElementById('totalAmount').value = cartTotalValue;
}

document.addEventListener('DOMContentLoaded', () => {
    const cart = JSON.parse(localStorage.getItem('cart')) || {};
    populateHiddenCartInputs(cart);
});