<template>
  <h1>Todo List</h1>
  <div class="todo-list">
    <todoItem v-for="(item,index) in items" :key="index"
      :item=item
      @toggleComplete="toggleComplete(item)"
    />
  </div>
  <div class="mg-t-12">
    <input name="newText" 
        title="Enter item to add"
        v-model="newDescription"/>
    <button v-on:click="AddItem"
      type="button" class="btn btn-resize bg-green">
      <i class="fas fa-plus"></i> Add Item
      </button>
  </div>
</template>

<script>
// @ is an alias to /src
import itemService from "@/services/itemService.js"
import todoItem from "@/components/todoItem"

export default {
  name: 'Home',
  components:{todoItem},
  data: function(){
    return {
      items: [],
      newDescription: ""
    }
  },
  created(){
    // get data
    this.GetItems();
  },
  methods : {
    async GetItems(){
      var result = await itemService.GetList();
      this.items = result.value;
    },
    async AddItem(){
      var addResult = await itemService.Add(this.newDescription);
      if(addResult.success){
        this.newText = "";
        this.GetItems();
      }
    },
    async toggleComplete(item){
      var toggleResult = await itemService.ToggleComplete(item.id);
      if(toggleResult.success){
        this.replaceItem(item,toggleResult.value)
      }
    },
    replaceItem(item,newItem){
      var index = this.items.findIndex(i => i.id == item.id);
      if(index < 0){
        return;
      }
      this.items.splice(index,1,newItem);
    }
  }
}
</script>
